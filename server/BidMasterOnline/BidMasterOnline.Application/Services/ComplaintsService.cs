using BidMasterOnline.Application.DTO;
using BidMasterOnline.Application.ServiceContracts;

namespace BidMasterOnline.Application.Services
{
    public class ComplaintsService : IComplaintsService
    {
        public Task<ComplaintDTO> GetComplaintById(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<ListModel<ComplaintDTO>> GetComplaintsList(ComplaintSpecificationsDTO specifications)
        {
            throw new NotImplementedException();
        }

        public Task HandleComplaintAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task SetNewComplaint(SetComplaintDTO complaint)
        {
            throw new NotImplementedException();
        }
    }
}
