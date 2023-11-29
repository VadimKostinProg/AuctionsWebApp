using System.ComponentModel.DataAnnotations;

namespace BidMasterOnline.Application.DTO
{
    /// <summary>
    /// DTO for registering new customer users. (REQUEST)
    /// </summary>
    public class RegisterDTO
    {
        [Required(ErrorMessage = "User name is required.")]
        public string UserName { get; set; } = null!;

        [Required(ErrorMessage = "Full name is required.")]
        public string FullName { get; set; } = null!;

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress]
        public string Email { get; set; } = null!;

        [Required(ErrorMessage = "Password is required.")]
        public string Password { get; set; } = null!;

        [Required(ErrorMessage = "Password confirmation is required.")]
        [Compare(nameof(Password), ErrorMessage = "Passwords do not match.")]
        public string ConfirmPassword { get; set; } = null!;
    }
}
