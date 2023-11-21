using BidMasterOnline.Application.Enums;

namespace BidMasterOnline.Application.DTO
{
    public class UserSpecifications : Specifications
    {
        public UserRole? Role;
        public UserStatus? Status;
    }
}
