using BidMasterOnline.Application.DTO;
using BidMasterOnline.Application.ServiceContracts;

namespace BidMasterOnline.Application.Services
{
    public class AuctionVerificationService : IAuctionVerificationService
    {
        public Task ApproveAuctionAsync(Guid auctionId)
        {
            throw new NotImplementedException();
        }

        public Task<ListModel<AuctionDTO>> GetNotApprovedAuctionsListAsync(SpecificationsDTO specifications)
        {
            throw new NotImplementedException();
        }

        public Task RejectAuctionAsync(Guid auctionId)
        {
            throw new NotImplementedException();
        }
    }
}
