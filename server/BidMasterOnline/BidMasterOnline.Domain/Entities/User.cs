using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace BidMasterOnline.Domain.Entities
{
    public class User : EntityBase
    {
        [Required]
        [MaxLength(30)]
        public string UserName { get; set; } = null!;

        [Required]
        [MaxLength(50)]
        public string FullName { get; set; } = null!;

        [Required]
        public DateTime DateOfBirth { get; set; }

        [Required]
        [MaxLength(30)]
        public string Email { get; set; } = null!;

        [Required]
        [MaxLength(50)]
        public string PasswordHashed { get; set; } = null!;

        [Required]
        [MaxLength(50)]
        public string PasswordSalt { get; set; } = null!;

        [AllowNull]
        public DateTime? UnblockDateTime { get; set; }

        [Required]
        public Guid UserStatusId { get; set; }

        public virtual ICollection<UserRole> UserRoles { get; set; }
    }
}
