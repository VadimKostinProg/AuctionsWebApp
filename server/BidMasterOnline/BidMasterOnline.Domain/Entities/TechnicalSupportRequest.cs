using System.ComponentModel.DataAnnotations;

namespace BidMasterOnline.Domain.Entities
{
    public class TechnicalSupportRequest : EntityBase
    {
        [Required]
        public Guid UserId { get; set; }

        [Required]
        [MaxLength(500)]
        public string RequestText { get; set; }

        [Required]
        public DateTime DateAndTime { get; set; }

        [Required]
        public bool IsHandled { get; set; }
    }
}
