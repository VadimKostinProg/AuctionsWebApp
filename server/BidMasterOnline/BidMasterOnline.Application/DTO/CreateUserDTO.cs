using BidMasterOnline.Application.Enums;
using System.ComponentModel.DataAnnotations;

namespace BidMasterOnline.Application.DTO
{
    /// <summary>
    /// DTO for creating new admin users. (REQUEST)
    /// </summary>
    public class CreateUserDTO : RegisterDTO
    {
        [Required(ErrorMessage = "User role is required.")]
        public UserRole Role { get; set; }
    }
}
