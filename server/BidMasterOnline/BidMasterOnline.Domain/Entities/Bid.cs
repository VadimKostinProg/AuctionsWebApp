using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BidMasterOnline.Domain.Entities
{
    public class Bid : EntityBase
    {
        [Required]
        public Guid AuctionId { get; set; }

        [Required]
        public Guid BidderId { get; set; }

        [Required]
        public DateTime DateAndTime { get; set; }

        [Required]
        public decimal Amount { get; set; }

        [Required]
        public bool IsWinning { get; set; }

        [ForeignKey(nameof(AuctionId))]
        public virtual Auction Auction { get; set; }

        [ForeignKey(nameof(BidderId))]
        public virtual User Bidder { get; set; }
    }
}
