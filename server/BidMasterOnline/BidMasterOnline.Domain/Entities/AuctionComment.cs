using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BidMasterOnline.Domain.Entities
{
    public class AuctionComment : EntityBase 
    {
        [Required]
        public Guid UserId { get; set; }

        [Required]
        public Guid AuctionId { get; set; }

        [Required]
        public DateTime DateAndTime { get; set; }

        [Required]
        [MaxLength(300)]
        public string CommentText { get; set; }

        public bool IsDeleted { get; set; }

        [ForeignKey(nameof(UserId))]
        public virtual User User { get; set; }

        [ForeignKey(nameof(AuctionId))]
        public virtual Auction Auction { get; set; }
    }
}
