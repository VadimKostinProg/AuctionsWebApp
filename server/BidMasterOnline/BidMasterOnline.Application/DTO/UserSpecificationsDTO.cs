using BidMasterOnline.Application.Enums;

namespace BidMasterOnline.Application.DTO
{
    public class UserSpecificationsDTO : SpecificationsDTO
    {
        public UserRole? Role;
        public UserStatus? Status;
    }
}
