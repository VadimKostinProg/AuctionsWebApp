﻿using AutoMapper;
using BidMasterOnline.Application.DTO;
using BidMasterOnline.Application.Exceptions;
using BidMasterOnline.Application.RepositoryContracts;
using BidMasterOnline.Application.ServiceContracts;
using BidMasterOnline.Application.Specifications;
using BidMasterOnline.Domain.Entities;

namespace BidMasterOnline.Application.Services
{
    public class BidsService : IBidsService
    {
        private readonly IAsyncRepository _repository;
        private readonly IJWTService _jwtService;

        public BidsService(IAsyncRepository repository, IJWTService jwtService)
        {
            _repository = repository;
            _jwtService = jwtService;
        }

        public async Task<ListModel<BidDTO>> GetBidsListForAuctionAsync(Guid auctionId, SpecificationsDTO specifications)
        {
            if (specifications is null)
                throw new ArgumentNullException("Specifications are null.");

            if (!await _repository.AnyAsync<Auction>(x => x.Id == auctionId))
                throw new KeyNotFoundException("Auction with such id does not exist.");

            var specification = new SpecificationBuilder<Bid>()
                .With(x => x.AuctionId == auctionId)
                .OrderBy(x => x.DateAndTime, Enums.SortOrder.DESC)
                .WithPagination(specifications.PageSize, specifications.PageNumber)
                .Build();

            var bids = await _repository.GetAsync<Bid>(specification, disableTracking: false);

            var totalCount = await _repository.CountAsync<Bid>(x => x.AuctionId == auctionId);

            var totalPages = (long)Math.Ceiling((double) totalCount / specifications.PageSize);

            var list = new ListModel<BidDTO>
            {
                List = bids.Select(x => new BidDTO
                {
                    Id = x.Id,
                    AuctionId = x.AuctionId,
                    AuctionName = x.Auction.Name,
                    BidderId = x.BidderId,
                    BidderUserName = x.Bidder.UserName,
                    DateAndTime = x.DateAndTime,
                    Amount = x.Amount
                })
                .ToList(),
                TotalPages = totalPages
            };

            return list;
        }

        public async Task<ListModel<BidDTO>> GetBidsListForUserAsync(Guid userId, BidSpecificationsDTO specifications)
        {
            if (specifications is null)
                throw new ArgumentNullException("Specifications are null.");

            if (!await _repository.AnyAsync<User>(x => x.Id == userId))
                throw new KeyNotFoundException("User with such id does not exist.");

            var specificationBuilder = new SpecificationBuilder<Bid>()
                .With(x => x.BidderId == userId)
                .OrderBy(x => x.DateAndTime, Enums.SortOrder.DESC)
                .WithPagination(specifications.PageSize, specifications.PageNumber);

            if (specifications.OnlyWinning)
                specificationBuilder.With(x => x.IsWinning == true);

            var specification = specificationBuilder.Build();

            var bids = await _repository.GetAsync<Bid>(specification, disableTracking: false);

            var totalCount = specifications.OnlyWinning ?
                await _repository.CountAsync<Bid>(x => x.BidderId == userId && x.IsWinning == true) :
                await _repository.CountAsync<Bid>(x => x.BidderId == userId);

            var totalPages = (long)Math.Ceiling((double)totalCount / specifications.PageSize);

            var list = new ListModel<BidDTO>
            {
                List = bids.Select(x => new BidDTO
                {
                    Id = x.Id,
                    AuctionId = x.AuctionId,
                    AuctionName = x.Auction.Name,
                    BidderId = x.BidderId,
                    BidderUserName = x.Bidder.UserName,
                    DateAndTime = x.DateAndTime,
                    Amount = x.Amount
                })
                .ToList(),
                TotalPages = totalPages
            };

            return list;
        }

        public async Task SetBidAsync(SetBidDTO bid)
        {
            // Validating bid
            if (bid is null)
                throw new ArgumentNullException("Bid is null.");

            var user = await _jwtService.GetAuthorizedUserAsync();

            if (user.Status == Enums.UserStatus.Blocked)
                throw new UserBlockedException("Account is blocked.");

            var auction = await _repository.GetByIdAsync<Auction>(bid.AuctionId, disableTracking: false);

            if (auction is null)
                throw new KeyNotFoundException("Auction with such id does not exist.");

            if (auction.FinishDateTime <= DateTime.UtcNow)
                throw new InvalidOperationException("Auction has already finished.");

            if (auction.Status.Name == Enums.AuctionStatus.Canceled.ToString())
                throw new InvalidOperationException("Auction is canceled.");

            if (auction.Bids.Any() && auction.Bids.Max(x => x.Amount) > bid.Amount)
                throw new ArgumentException("Bid is less than previous one.");
            else if (auction.StartPrice > bid.Amount)
                throw new ArgumentException("Bid is less than start price.");

            // Setting new bid
            var bidToSet = new Bid
            {
                AuctionId = bid.AuctionId,
                BidderId = user.Id,
                DateAndTime = DateTime.UtcNow,
                Amount = bid.Amount,
            };

            await _repository.AddAsync(bidToSet);

            if (auction.FinishType.Name == Enums.AuctionFinishType.IncreasingFinishTime.ToString())
            {
                auction.FinishDateTime.Add(auction.FinishInterval!.Value);

                await _repository.UpdateAsync(auction);
            }
        }
    }
}
