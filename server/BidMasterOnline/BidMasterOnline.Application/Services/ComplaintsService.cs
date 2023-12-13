using BidMasterOnline.Application.DTO;
using BidMasterOnline.Application.ServiceContracts;

namespace BidMasterOnline.Application.Services
{
    public class ComplaintsService : IComplaintsService
    {
        public Task<ComplaintDTO> GetComplaintByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<ListModel<ComplaintDTO>> GetComplaintsListAsync(ComplaintSpecificationsDTO specifications)
        {
            throw new NotImplementedException();
        }

        public Task HandleComplaintAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task SetNewComplaintAsync(SetComplaintDTO complaint)
        {
            throw new NotImplementedException();
        }
    }
}
