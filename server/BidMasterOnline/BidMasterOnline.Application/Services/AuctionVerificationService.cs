using BidMasterOnline.Application.DTO;
using BidMasterOnline.Application.RepositoryContracts;
using BidMasterOnline.Application.ServiceContracts;
using BidMasterOnline.Application.Specifications;
using BidMasterOnline.Domain.Entities;

namespace BidMasterOnline.Application.Services
{
    public class AuctionVerificationService : IAuctionVerificationService
    {
        private readonly IAsyncRepository _repository;
        private readonly INotificationsService _notificationsService;
        private readonly IImagesService _imagesService;

        public AuctionVerificationService(IAsyncRepository repository, INotificationsService notificationsService, 
            IImagesService imagesService)
        {
            _repository = repository;
            _notificationsService = notificationsService;
            _imagesService = imagesService;
        }

        public async Task ApproveAuctionAsync(Guid auctionId)
        {
            var auction = await _repository.FirstOrDefaultAsync<Auction>(x => x.Id == auctionId && !x.IsApproved);

            if (auction is null)
                throw new KeyNotFoundException("Auction with such id does not exist.");

            auction.IsApproved = true;
            auction.StartDateTime = DateTime.Now;
            auction.FinishDateTime = DateTime.Now.Add(auction.AuctionTime);

            await _repository.UpdateAsync(auction);

            _notificationsService.SendMessageOfApprovalAuctionToAuctionist(auction);
        }

        public async Task<AuctionDetailsDTO> GetNotApprovedAuctionDetailsByIdAsync(Guid id)
        {
            var auction = await _repository.FirstOrDefaultAsync<Auction>(x => x.Id == id && !x.IsApproved);

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
                FinishTypeDescription = auction.FinishType.Description,
                Status = Enum.Parse<Enums.AuctionStatus>(auction.Status.Name),
            };

            return auctionDetailsDTO;
        }

        public async Task<ListModel<AuctionDTO>> GetNotApprovedAuctionsListAsync(SpecificationsDTO specifications)
        {
            if (specifications is null)
                throw new ArgumentNullException(nameof(specifications));

            var specification = new SpecificationBuilder<Auction>()
                .With(x => x.IsApproved == false)
                .OrderBy(x => x.StartDateTime, Enums.SortDirection.ASC)
                .WithPagination(specifications.PageSize, specifications.PageNumber)
                .Build();

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

        public async Task RejectAuctionAsync(RejectAuctionDTO request)
        {
            var auction = await _repository.FirstOrDefaultAsync<Auction>(x => 
                x.Id == request.AuctionId && !x.IsApproved);

            if (auction is null)
                throw new KeyNotFoundException("Auction with such id does not exist.");

            await this.DeleteAuctionImagesAsync(auction);

            await _repository.DeleteAsync(auction);

            _notificationsService.SendMessageOfRejectionAuctionToAuctionist(auction, request.RejectionReason);
        }

        private async Task DeleteAuctionImagesAsync(Auction auction)
        {
            var imagePublicIds = auction.Images.Select(x => x.PublicId);

            foreach(var publicId in imagePublicIds)
            {
                await _imagesService.DeleteImageAsync(publicId);
            }
        }
    }
}
