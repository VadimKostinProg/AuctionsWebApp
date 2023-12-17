using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace BidMasterOnline.Domain.Entities
{
    public class User : EntityBase
    {
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

        [ForeignKey(nameof(RoleId))]
        public virtual Role Role { get; set; }

        [ForeignKey(nameof(UserStatusId))]
        public virtual UserStatus UserStatus { get; set; }
    }
}
