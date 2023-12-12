using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace BidMasterOnline.Domain.Entities
{
    public class Auction : EntityBase
    {
        [Required]
        [MaxLength(50)]
        public string Name { get; set; } = null!;

        [Required]
        public Guid AuctionistId { get; set; }

        [Required]
        public Guid CategoryId { get; set; }

        [Required]
        [MaxLength(300)]
        public string LotDescription { get; set; } = null!;

        [Required]
        public DateTime StartDateTime { get; set; }

        [Required]
        public DateTime FinishDateTime { get; set; }

        [AllowNull]
        public TimeSpan? FinishInterval { get; set; }

        [Required]
        public decimal StartPrice { get; set; }

        [Required]
        public Guid FinishTypeId { get; set; }

        [Required]
        public Guid StatusId { get; set; }

        public bool IsApproved { get; set; }

        public bool IsPayed { get; set; }

        [AllowNull]
        public DateTime? PaymentDateTime { get; set; }

        [ForeignKey(nameof(AuctionistId))]
        public virtual User Auctionist { get; set; }

        [ForeignKey(nameof(CategoryId))]
        public virtual Category Category { get; set; }

        [ForeignKey(nameof(FinishTypeId))]
        public virtual AuctionFinishType FinishType { get; set; }

        [ForeignKey(nameof(StatusId))]
        public virtual AuctionStatus Status { get; set; }

        public virtual ICollection<AuctionImage> Images { get; set; }

        public virtual ICollection<Bid> Bids { get; set; }
    }
}
