using Microsoft.EntityFrameworkCore.Infrastructure;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BidMasterOnline.Domain.Entities
{
    public class Bid : EntityBase
    {
        private ILazyLoader _loader;

        public Bid() { }

        public Bid(ILazyLoader loader)
        {
            _loader = loader;
        }

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

        #region NAVIGATION FIELDS
        private Auction _auction;
        private User _bidder;

        public virtual Auction Auction
        {
            get => _loader.Load(this, ref _auction);
            set => _auction = value;
        }

        
        public virtual User Bidder
        {
            get => _loader.Load(this, ref _bidder);
            set => _bidder = value;
        }
        #endregion
    }
}
