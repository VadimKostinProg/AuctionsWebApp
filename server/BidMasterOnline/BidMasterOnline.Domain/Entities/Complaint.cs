using Microsoft.EntityFrameworkCore.Infrastructure;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace BidMasterOnline.Domain.Entities
{
    public class Complaint : EntityBase
    {
        private readonly ILazyLoader _loader;

        public Complaint() { }

        public Complaint(ILazyLoader loader)
        {
            _loader = loader;
        }

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

        #region NAVIGATION FIELDS
        private User _accusedUser;
        private User _accusingUser;
        private Auction _auction;
        private AuctionComment _comment;
        private ComplaintType _complaintType;

        public User AccusedUser
        {
            get => _loader.Load(this, ref _accusedUser);
            set => _accusedUser = value;
        }

        public User AccusingUser
        {
            get => _loader.Load(this, ref _accusingUser);
            set => _accusingUser = value;
        }

        public virtual Auction Auction
        {
            get => _loader.Load(this, ref _auction);
            set => _auction = value;
        }

        public AuctionComment Comment
        {
            get => _loader.Load(this, ref _comment);
            set => _comment = value;
        }

        public ComplaintType ComplaintType
        {
            get => _loader.Load(this, ref _complaintType);
            set => _complaintType = value;
        }
        #endregion
    }
}
