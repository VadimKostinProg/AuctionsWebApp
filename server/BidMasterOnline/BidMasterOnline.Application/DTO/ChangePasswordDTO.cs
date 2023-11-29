using System.ComponentModel.DataAnnotations;

namespace BidMasterOnline.Application.DTO
{
    /// <summary>
    /// DTO for changing user`s password. (REQUEST)
    /// </summary>
    public class ChangePasswordDTO
    {
        [Required(ErrorMessage = "Id is required.")]
        public Guid UserId { get; set; }

        [Required(ErrorMessage = "Current password is required")]
        public string CurrentPassword { get; set; } = null!;

        [Required(ErrorMessage = "New password is required")]
        public string NewPassword { get; set; } = null!;

        [Required(ErrorMessage = "New password confirmation is required.")]
        [Compare(nameof(NewPassword), ErrorMessage = "Passwords do not match.")]
        public string ConfirmedNewPassword { get; set; } = null!;
    }
}
