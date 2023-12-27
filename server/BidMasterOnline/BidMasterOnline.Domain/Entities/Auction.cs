using Microsoft.EntityFrameworkCore.Infrastructure;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace BidMasterOnline.Domain.Entities
{
    public class Auction : EntityBase
    {
        private readonly ILazyLoader _loader;

        public Auction() { }

        public Auction(ILazyLoader loader)
        {
            _loader = loader;
        }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; } = null!;

        [Required]
        public Guid AuctionistId { get; set; }

        [Required]
        public Guid CategoryId { get; set; }

        [Required]
        [MaxLength(1000)]
        public string LotDescription { get; set; } = null!;

        [Required]
        public DateTime StartDateTime { get; set; }

        [Required]
        public DateTime FinishDateTime { get; set; }

        [Required]
        public long AuctionTime { get; set; }

        [AllowNull]
        public long? FinishInterval { get; set; }

        [Required]
        public decimal StartPrice { get; set; }

        [Required]
        public Guid FinishTypeId { get; set; }

        [Required]
        public Guid StatusId { get; set; }

        public bool IsApproved { get; set; }

        #region NAVIGATION FIELDS
        private User _auctionist;
        private Category _category;
        private AuctionFinishType _finishType;
        private AuctionStatus _status;
        private AuctionPaymentDeliveryOptions _paymentDeliveryOptions;

        private ICollection<AuctionImage> _images;
        private ICollection<Bid> _bids;
        private ICollection<AuctionScore> _scores;

        public User Auctionist
        {
            get => _loader.Load(this, ref _auctionist);
            set => _auctionist = value;
        }

        public Category Category
        {
            get => _loader.Load(this, ref _category);
            set => _category = value;
        }

        public AuctionFinishType FinishType
        {
            get => _loader.Load(this, ref _finishType);
            set => _finishType = value;
        }

        public AuctionStatus Status
        {
            get => _loader.Load(this, ref _status);
            set => _status = value;
        }

        public AuctionPaymentDeliveryOptions PaymentDeliveryOptions
        {
            get => _loader.Load(this, ref _paymentDeliveryOptions);
            set => _paymentDeliveryOptions = value;
        }

        public ICollection<AuctionImage> Images
        {
            get => _loader.Load(this, ref _images);
            set => _images = value;
        }

        public ICollection<Bid> Bids
        {
            get => _loader.Load(this, ref _bids);
            set => _bids = value;
        }

        public ICollection<AuctionScore> Scores
        {
            get => _loader.Load(this, ref _scores);
            set => _scores = value;
        }
        #endregion
    }
}
