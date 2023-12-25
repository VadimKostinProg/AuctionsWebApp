using Microsoft.EntityFrameworkCore.Infrastructure;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BidMasterOnline.Domain.Entities
{
    public class AuctionComment : EntityBase 
    {
        private readonly ILazyLoader _loader;

        public AuctionComment() { }

        public AuctionComment(ILazyLoader loader)
        {
            _loader = loader;
        }

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

        #region NAVIGATION FIELDS
        private User _user;
        private Auction _auction;

        public User User
        {
            get => _loader.Load(this, ref _user);
            set => _user = value;
        }

        public Auction Auction
        {
            get => _loader.Load(this, ref _auction);
            set => _auction = value;
        }
        #endregion
    }
}
