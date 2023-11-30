using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace BidMasterOnline.Domain.Entities
{
    public class Complaint : EntityBase
    {
        [Required]
        public Guid AccusingUserId { get; set; }

        [Required]
        public Guid AccusedUserId { get; set; }

        [Required]
        public Guid AuctionId { get; set; }

        [AllowNull]
        public Guid? CommentId { get; set; }

        [Required]
        public Guid ComplaintTypeId { get; set; }

        [Required]
        public DateTime DateAndTime { get; set; }

        [Required]
        [MaxLength(300)]
        public string ComplaintText { get; set; } = null!;

        public bool IsHandled { get; set; }

        [ForeignKey(nameof(AccusedUserId))]
        public virtual User AccusedUser { get; set; }

        [ForeignKey(nameof(AccusingUserId))]
        public virtual User AccusingUser { get; set; }

        [ForeignKey(nameof(AuctionId))]
        public virtual Auction Auction { get; set; }

        [ForeignKey(nameof(CommentId))]
        public virtual Comment Comment { get; set; }

        [ForeignKey(nameof(ComplaintTypeId))]
        public virtual ComplaintType ComplaintType { get; set; }
    }
}
