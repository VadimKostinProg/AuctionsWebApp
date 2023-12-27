using Microsoft.EntityFrameworkCore.Infrastructure;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace BidMasterOnline.Domain.Entities
{
    public class User : EntityBase
    {
        private readonly ILazyLoader _loader;

        public User() { }

        public User(ILazyLoader loader)
        {
            _loader = loader;
        }

        [Required]
        [MaxLength(30)]
        public string Username { get; set; } = null!;

        [Required]
        [MaxLength(50)]
        public string FullName { get; set; } = null!;

        [Required]
        public DateTime DateOfBirth { get; set; }

        [Required]
        [MaxLength(30)]
        public string Email { get; set; } = null!;

        public bool IsEmailConfirmed { get; set; }

        [Required]
        public string PasswordHashed { get; set; } = null!;

        [Required]
        public string PasswordSalt { get; set; } = null!;

        [AllowNull]
        public DateTime? UnblockDateTime { get; set; }

        [Required]
        public Guid RoleId { get; set; }

        [Required]
        public Guid UserStatusId { get; set; }

        [AllowNull]
        public string? ImageUrl { get; set; }

        [AllowNull]
        public string? ImagePublicId { get; set; }

        #region NAVIGATION FIELDS
        private Role _role;
        private UserStatus _userStatus;

        public Role Role
        {
            get => _loader.Load(this, ref _role);
            set => _role = value;
        }

        public UserStatus UserStatus
        {
            get => _loader.Load(this, ref _userStatus);
            set => _userStatus = value;
        }
        #endregion
    }
}
