using BidMasterOnline.Application.DTO;
using BidMasterOnline.Application.ServiceContracts;

namespace BidMasterOnline.Application.Services
{
    public class AuctionsService : IAuctionsService
    {
        public Task CancelAuctionAsync(Guid auctionId)
        {
            throw new NotImplementedException();
        }

        public Task ConfirmPaymentForAuctionAsync(Guid auctionId)
        {
            throw new NotImplementedException();
        }

        public Task<AuctionDTO> GetAuctionByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<AuctionDetailsDTO> GetAuctionDetailsByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<ListModel<AuctionDTO>> GetAuctionsListAsync(AuctionSpecificationsDTO specifications)
        {
            throw new NotImplementedException();
        }

        public Task PublishAuctionAsync(PublishAuctionDTO request)
        {
            throw new NotImplementedException();
        }

        public Task RecoverAuctionAsync(Guid auctionId, DateTime? newFinishTime)
        {
            throw new NotImplementedException();
        }
    }
}
