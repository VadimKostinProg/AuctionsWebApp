using System.ComponentModel.DataAnnotations;

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
        [MaxLength(30)]
        public string Email { get; set; } = null!;

        [Required]
        [MaxLength(50)]
        public string PasswordHashed { get; set; } = null!;

        [Required]
        public Guid UserStatusId { get; set; }

        public virtual ICollection<UserRole> UserRoles { get; set; }
    }
}
