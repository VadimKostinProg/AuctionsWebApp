﻿using BidMasterOnline.Application.DTO;
using BidMasterOnline.Application.Exceptions;
using BidMasterOnline.Application.RepositoryContracts;
using BidMasterOnline.Application.ServiceContracts;
using BidMasterOnline.Application.Specifications;
using BidMasterOnline.Domain.Entities;
using Microsoft.AspNetCore.Http;

namespace BidMasterOnline.Application.Services
{
    public class AuctionsService : IAuctionsService
    {
        private readonly IAsyncRepository _repository;
        private readonly IAuthService _authService;
        private readonly INotificationsService _notificationsService;
        private readonly IImagesService _imagesService;

        public AuctionsService(IAsyncRepository repository, IAuthService authService,
            INotificationsService notificationsService, IImagesService imagesService)
        {
            _repository = repository;
            _authService = authService;
            _notificationsService = notificationsService;
            _imagesService = imagesService;
        }

        public async Task CancelAuctionAsync(CancelAuctionDTO request)
        {
            if (request is null)
                throw new ArgumentException("Request object is null.");

            var auction = await _repository.GetByIdAsync<Auction>(request.AuctionId);

            if (auction is null)
                throw new KeyNotFoundException("Auction with such id does not exist.");

            if (auction.Status.Name == Enums.AuctionStatus.Canceled.ToString())
                throw new InvalidOperationException("Auction is already canceled.");

            if (auction.Status.Name == Enums.AuctionStatus.Finished.ToString())
                throw new InvalidOperationException("Auction is finished.");

            await this.PerformCancelingForAuctionAsync(auction);

            _notificationsService.SendMessageOfCancelingAuctionToAuctionist(auction, request.CancelingReason);
        }

        public async Task CancelOwnAuctionAsync(Guid auctionId)
        {
            var auction = await _repository.GetByIdAsync<Auction>(auctionId);

            if (auction is null)
                throw new KeyNotFoundException("Auction with such id does not exist.");

            if (auction.Status.Name == Enums.AuctionStatus.Canceled.ToString())
                throw new InvalidOperationException("Auction is already canceled.");

            if (auction.Status.Name == Enums.AuctionStatus.Finished.ToString())
                throw new InvalidOperationException("Auction is finished.");

            var user = await _authService.GetAuthenticatedUserEntityAsync();

            if (auction.AuctionistId != user.Id)
                throw new ForbiddenException("You cannot cancel the auction of other user.");

            await this.PerformCancelingForAuctionAsync(auction);
        }

        private async Task PerformCancelingForAuctionAsync(Auction auction)
        {
            var bids = auction.Bids;

            await _repository.DeleteManyAsync<Bid>(bids);

            var status = await _repository.FirstOrDefaultAsync<AuctionStatus>(x =>
                x.Name == Enums.AuctionStatus.Canceled.ToString());

            auction.StatusId = status!.Id;

            await _repository.UpdateAsync(auction);
        }

        public async Task<AuctionDTO> GetAuctionByIdAsync(Guid id)
        {
            var auction = await _repository.GetByIdAsync<Auction>(id);

            if (auction is null)
                throw new KeyNotFoundException("Auction with such id does not exist.");

            var auctionDTO = new AuctionDTO
            {
                Id = auction.Id,
                Name = auction.Name,
                FinishDateAndTime = auction.FinishDateTime,
                StartPrice = auction.StartPrice,
                CurrentBid = auction.Bids.Any() ? auction.Bids.Max(x => x.Amount) : 0,
                ImageUrls = auction.Images.Select(x => x.Url).ToList()
            };

            return auctionDTO;
        }

        public async Task<AuctionDetailsDTO> GetAuctionDetailsByIdAsync(Guid id)
        {
            var auction = await _repository.FirstOrDefaultAsync<Auction>(x => x.Id == id && x.IsApproved);

            if (auction is null)
                throw new KeyNotFoundException("Auction with such id does not exist.");

            var auctionDetailsDTO = new AuctionDetailsDTO
            {
                Id = auction.Id,
                Name = auction.Name,
                FinishDateAndTime = auction.FinishDateTime,
                StartPrice = auction.StartPrice,
                CurrentBid = auction.Bids.Any() ? auction.Bids.Max(x => x.Amount) : 0,
                ImageUrls = auction.Images.Select(x => x.Url).ToList(),
                Category = auction.Category.Name,
                AuctionistName = auction.Auctionist.Username,
                StartDateAndTime = auction.StartDateTime,
                LotDescription = auction.LotDescription,
                Score = auction.Scores.Average(x => x.Score),
                FinishTypeDescription = auction.FinishType.Description,
                Status = Enum.Parse<Enums.AuctionStatus>(auction.Status.Name),
                Bids = auction.Bids
                    .Select(bid => new BidDTO
                    {
                        Id = bid.Id,
                        AuctionId = auction.Id,
                        AuctionName = auction.Name,
                        BidderId = bid.BidderId,
                        BidderUsername = bid.Bidder.Username,
                        DateAndTime = bid.DateAndTime,
                        Amount = bid.Amount
                    })
                    .ToList()
            };

            return auctionDetailsDTO;
        }

        public async Task<ListModel<AuctionDTO>> GetAuctionsListAsync(AuctionSpecificationsDTO specifications)
        {
            if (specifications is null)
                throw new ArgumentNullException("Specifications are null.");

            if (specifications.CategoryId is not null &&
                !await _repository.AnyAsync<Category>(x => x.Id == specifications.CategoryId && x.IsDeleted == false))
                throw new KeyNotFoundException("Category with such id does not exists.");

            var specification = this.GetSpecification(specifications);

            var auctions = await _repository.GetAsync<Auction>(specification);

            var totalCount = await _repository.CountAsync<Auction>(specification.Predicate);

            var totalPages = (long)Math.Ceiling((double)totalCount / specifications.PageSize);

            var list = new ListModel<AuctionDTO>
            {
                List = auctions
                    .Select(auction => new AuctionDTO
                    {
                        Id = auction.Id,
                        Name = auction.Name,
                        FinishDateAndTime = auction.FinishDateTime,
                        StartPrice = auction.StartPrice,
                        CurrentBid = auction.Bids.Any() ? auction.Bids.Max(x => x.Amount) : 0,
                        ImageUrls = auction.Images.Select(x => x.Url).ToList()
                    })
                    .ToList(),
                TotalPages = totalPages
            };

            return list;
        }

        private ISpecification<Auction> GetSpecification(AuctionSpecificationsDTO specifications)
        {
            var builder = new SpecificationBuilder<Auction>();

            builder.With(x => x.IsApproved == true);

            if (specifications.CategoryId is not null)
                builder.With(x => x.CategoryId == specifications.CategoryId);

            if (specifications.MinStartPrice is not null)
                builder.With(x => x.StartPrice >= specifications.MinStartPrice && x.StartPrice <= specifications.MaxStartPrice!.Value);

            if (specifications.MinCurrentBid is not null)
                builder.With(x => x.Bids.Max(x => x.Amount) >= specifications.MinCurrentBid && x.Bids.Max(x => x.Amount) <= specifications.MaxCurrentBid!.Value);

            if (specifications.Status is not null)
                builder.With(x => x.Status.Name == specifications.Status.ToString());

            if (!string.IsNullOrEmpty(specifications.SearchTerm))
                builder.With(x => x.Name.Contains(specifications.SearchTerm));

            if (!string.IsNullOrEmpty(specifications.SortField))
            {
                switch (specifications.SortField)
                {
                    case "Popularity":
                        builder.OrderBy(x => x.Bids.Count(), specifications.SortDirection ?? Enums.SortDirection.ASC);
                        break;
                    case "DateAndTime":
                        builder.OrderBy(x => x.FinishDateTime, specifications.SortDirection ?? Enums.SortDirection.DESC);
                        break;
                }
            }

            builder.WithPagination(specifications.PageSize, specifications.PageNumber);

            return builder.Build();
        }

        public async Task PublishAuctionAsync(PublishAuctionDTO request)
        {
            if (request is null)
                throw new ArgumentNullException("Auction is null.");

            if (string.IsNullOrEmpty(request.Name))
                throw new ArgumentNullException("Auction name is blank.");

            if (string.IsNullOrEmpty(request.LotDescription))
                throw new ArgumentNullException("Lot description is blank.");

            if (request.FinishType == Enums.AuctionFinishType.IncreasingFinishTime && request.FinishTimeInterval is null)
                throw new ArgumentNullException("Finish time interval is blank.");

            if (!await _repository.AnyAsync<Category>(x => x.Id == request.CategoryId))
                throw new KeyNotFoundException("Category with such id does not exist.");

            var auctionist = await _authService.GetAuthenticatedUserEntityAsync();

            if (auctionist.UserStatus.Name == Enums.UserStatus.Blocked.ToString())
                throw new ForbiddenException("Your account is blocked.");

            if (!auctionist.IsEmailConfirmed)
                throw new ForbiddenException("Your email is not confirmed.");

            var finishType = await _repository.FirstOrDefaultAsync<AuctionFinishType>(x =>
                x.Name == request.FinishType.ToString());

            var status = await _repository.FirstOrDefaultAsync<AuctionStatus>(x =>
                x.Name == Enums.AuctionStatus.Active.ToString());

            var auction = new Auction
            {
                Name = request.Name,
                AuctionistId = auctionist.Id,
                CategoryId = request.CategoryId,
                LotDescription = request.LotDescription,
                StartDateTime = DateTime.Now,
                FinishDateTime = DateTime.Now.Add(request.AuctionTime),
                AuctionTime = request.AuctionTime,
                FinishTypeId = finishType!.Id,
                FinishInterval = request.FinishType == Enums.AuctionFinishType.IncreasingFinishTime ? request.FinishTimeInterval : null,
                StartPrice = request.StartPrice,
                StatusId = status!.Id
            };

            await _repository.AddAsync(auction);

            await this.UploadAuctionImagesAsync(auction.Id, request.Images);

            _notificationsService.SendMessageOfPublishingAuctionToAuctionst(auction);
        }

        private async Task UploadAuctionImagesAsync(Guid auctionId, List<IFormFile> files)
        {
            foreach(var file in files)
            {
                var uploadResponse = await _imagesService.AddImageAsync(file);

                var auctionImage = new AuctionImage
                {
                    AuctionId = auctionId,
                    Url = uploadResponse.SecureUrl.AbsoluteUri,
                    PublicId = uploadResponse.PublicId
                };

                await _repository.AddAsync(auctionImage);
            }
        }

        public async Task RecoverAuctionAsync(Guid auctionId)
        {
            var auction = await _repository.GetByIdAsync<Auction>(auctionId);

            if (auction is null)
                throw new KeyNotFoundException("Auction with such id does not exist.");

            if (auction.Status.Name == Enums.AuctionStatus.Active.ToString())
                throw new InvalidOperationException("Auction is active.");

            if (auction.Status.Name == Enums.AuctionStatus.Finished.ToString())
                throw new InvalidOperationException("Auction is finished.");

            var status = await _repository.FirstOrDefaultAsync<AuctionStatus>(x =>
                x.Name == Enums.AuctionStatus.Active.ToString());

            auction.StatusId = status!.Id;
            auction.StartDateTime = DateTime.Now;
            auction.FinishDateTime = DateTime.Now.Add(auction.AuctionTime);

            await _repository.UpdateAsync(auction);

            _notificationsService.SendMessageOfRecoveringAuctionToAuctionist(auction);
        }

        public async Task SetAuctionScoreAsync(SetAuctionScoreDTO request)
        {
            var user = await _authService.GetAuthenticatedUserEntityAsync();

            if (user.UserStatus.Name == Enums.UserStatus.Blocked.ToString())
                throw new ForbiddenException("Your account is blocked.");

            var auction = await _repository.FirstOrDefaultAsync<Auction>(x => x.Id == request.AuctionId && x.IsApproved);

            if (auction is null)
                throw new KeyNotFoundException("Auction with such id does not exist.");

            if (auction.Status.Name == Enums.AuctionStatus.Canceled.ToString())
                throw new InvalidOperationException("Auction is canceled.");

            // If user has already set the score for auction before, update the score
            var existentScore = await _repository.FirstOrDefaultAsync<AuctionScore>(x =>
                x.UserId == user.Id && x.AuctionId == auction.Id);

            if (existentScore is not null)
            {
                existentScore.Score = request.Score;

                await _repository.UpdateAsync(existentScore);

                return;
            }

            // If user has not set the score for auction before, add new score
            var auctionScore = new AuctionScore
            {
                AuctionId = request.AuctionId,
                UserId = user.Id,
                Score = request.Score,
            };

            await _repository.AddAsync(auctionScore);
        }

        public async Task SetNextWinnerOfAuctionAsync(Guid auctionId)
        {
            var auction = await _repository.GetByIdAsync<Auction>(auctionId);

            if (auction is null)
                throw new KeyNotFoundException("Auction with such id does not exist.");

            if (auction.Status.Name == Enums.AuctionStatus.Canceled.ToString())
                throw new InvalidOperationException("Auction is canceled.");

            if (auction.Status.Name == Enums.AuctionStatus.Active.ToString())
                throw new InvalidOperationException("Cannot change the winner of auction before it is finished.");

            var bidsCount = auction.Bids.Count();

            if (bidsCount == 0)
            {
                throw new InvalidOperationException("Cannot set new winner of the auction as there are not any bids.");
            }

            await this.CancelWinnersBidForAuctionAsync(auction);

            if (bidsCount == 1)
            {
                await this.SetNoWinnersToAuctionAsync(auction);
            }
            else
            {
                await this.SetNextBidderAsWinnerAsync(auction);
            }
        }

        private async Task CancelWinnersBidForAuctionAsync(Auction auction)
        {
            var winnersBid = auction.Bids.First(x => x.IsWinning);

            _notificationsService.SendMessageOfCancelingTheBidToWinner(winnersBid);

            await _repository.DeleteAsync(winnersBid);
        }

        private async Task SetNextBidderAsWinnerAsync(Auction auction)
        {
            // Updating the winners bid
            var specification = new SpecificationBuilder<Bid>()
                .With(x => x.AuctionId == auction.Id)
                .OrderBy(x => x.Amount, Enums.SortDirection.DESC)
                .Build();

            var bids = await _repository.GetAsync<Bid>(specification);

            var newWinnersBid = bids.First();

            newWinnersBid.IsWinning = true;

            await _repository.UpdateAsync(newWinnersBid);

            // Updating sell and delivery options
            var paymentDeliveryOptions = auction.PaymentDeliveryOptions;

            paymentDeliveryOptions!.WinnerId = newWinnersBid.BidderId;

            await _repository.UpdateAsync(paymentDeliveryOptions);

            // Sending a letter to new winner
            var user = await _repository.GetByIdAsync<User>(newWinnersBid.BidderId);

            _notificationsService.SendMessageOfDeliveryOptionsSetToWinner(auction, user);
        }

        public async Task SetNoWinnersToAuctionAsync(Auction auction)
        {
            var paymentDeliveryOptions = auction.PaymentDeliveryOptions;

            paymentDeliveryOptions!.WinnerId = null;
            paymentDeliveryOptions.IBAN =
            paymentDeliveryOptions.Country =
            paymentDeliveryOptions.City =
            paymentDeliveryOptions.Waybill = null;
            paymentDeliveryOptions.AreDeliveryOptionsSet =
            paymentDeliveryOptions.ArePaymentOptionsSet = 
            paymentDeliveryOptions.IsDeliveryConfirmed = 
            paymentDeliveryOptions.IsPaymentConfirmed = false;

            await _repository.UpdateAsync(paymentDeliveryOptions);

            _notificationsService.SendMessageOfNoWinnersOfAuctionToAuctionist(auction);
        }
    }
}