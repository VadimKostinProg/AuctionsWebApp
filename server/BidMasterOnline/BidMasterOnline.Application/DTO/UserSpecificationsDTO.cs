using BidMasterOnline.Application.Enums;

namespace BidMasterOnline.Application.DTO
{
    /// <summary>
    /// DTO for user specifications for filtering, sorting and pagination. (REQUEST)
    /// </summary>
    public class UserSpecificationsDTO : SpecificationsDTO
    {
        public UserRole? Role;
        public UserStatus? Status;
    }
}
