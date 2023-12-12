using BidMasterOnline.Application.RepositoryContracts;
using BidMasterOnline.Application.ServiceContracts;
using BidMasterOnline.Application.Specifications;
using BidMasterOnline.Domain.Entities;
using Microsoft.Extensions.Logging;

namespace BidMasterOnline.Application.Services
{
    public class PeriodicTaskService : IPeriodicTaskService
    {
        private readonly IAsyncRepository _repository;
        private readonly INotificationsService _notificationsService;
        private readonly ILogger<PeriodicTaskService> _logger;

        public PeriodicTaskService(IAsyncRepository repository, INotificationsService notificationsService, 
            ILogger<PeriodicTaskService> logger)
        {
            _repository = repository;
            _notificationsService = notificationsService;
            _logger = logger;
        }

        public async Task PerformPeriodicTaskAsync()
        {
            _logger.LogInformation("--> PeriodicTaskService: Performing periodic operation has started.");

            _logger.LogInformation("--> PeriodicTaskService: applying finished status to auctions.");
            var auctionsAmount = await ApplyAuctionFinishingAsync();
            _logger.LogInformation($"--> PeriodicTaskService: {auctionsAmount} auctions were applyed finished status.");

            _logger.LogInformation("--> PeriodicTaskService: Performing periodic operation has finished.");
        }

        /// <summary>
        /// Applyes finished status to all actually finished auctions and sends the notification to the aucitonist and winner.
        /// </summary>
        /// <returns>Amount of finished auctions.</returns>
        private async Task<int> ApplyAuctionFinishingAsync()
        {
            var today = DateTime.UtcNow;
            var specification = new SpecificationBuilder<Auction>()
                .With(x => x.Status.Name == Enums.AuctionStatus.Active.ToString())
                .With(x => x.FinishDateTime < today)
                .Build();

            var auctionsToFinish = await _repository.GetAsync(specification, disableTracking: false);

            var finishedStatus = await _repository.FirstOrDefaultAsync<AuctionStatus>(x => x.Name == Enums.AuctionStatus.Finished.ToString(), disableTracking: false);

            foreach (var auction in auctionsToFinish)
            {
                auction.StatusId = finishedStatus.Id;
                await _repository.UpdateAsync(auction);

                var auctionist = auction.Auctionist;

                if (auction.Bids.Any())
                {
                    var winningBid = auction.Bids.OrderByDescending(x => x.DateAndTime).First();

                    winningBid.IsWinning = true;

                    await _repository.UpdateAsync(winningBid);

                    var winner = winningBid.Bidder;

                    await _notificationsService.SendNotificationAsync(auctionist.Email, "Auction finished.", "");
                    await _notificationsService.SendNotificationAsync(winner.Email, "Auction victory.", "");
                }
                else
                {
                    await _notificationsService.SendNotificationAsync(auctionist.Email, "Auction finished.", "");
                }
            }

            return auctionsToFinish.Count();
        }
    }
}
