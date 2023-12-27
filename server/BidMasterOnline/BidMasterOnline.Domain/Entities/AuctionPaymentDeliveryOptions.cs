using Microsoft.EntityFrameworkCore.Infrastructure;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace BidMasterOnline.Domain.Entities
{
    public class AuctionPaymentDeliveryOptions : EntityBase
    {
        private ILazyLoader _loader;

        public AuctionPaymentDeliveryOptions() { }

        public AuctionPaymentDeliveryOptions(ILazyLoader loader)
        {
            _loader = loader;
        }

        [Required]
        public Guid AuctionId { get; set; }

        [AllowNull]
        public Guid? WinnerId { get; set; }

        public bool ArePaymentOptionsSet { get; set; }

        [AllowNull]
        public DateTime? PaymentOptionsSetDateTime { get; set; }

        [AllowNull]
        public string? IBAN { get; set; }

        public bool AreDeliveryOptionsSet { get; set; }

        [AllowNull]
        public DateTime? DeliveryOptionsSetDateTime { get; set; }

        [AllowNull]
        public string? Country { get; set; }

        [AllowNull]
        public string? City { get; set; }

        [AllowNull]
        public string? ZipCode { get; set; }

        public bool IsPaymentConfirmed { get; set; }

        [AllowNull]
        public DateTime? PaymentConfirmationDateTime { get; set; }

        public bool IsDeliveryConfirmed { get; set; }

        [AllowNull]
        public DateTime? DeliveryConfirmationDateTime { get; set; }

        [AllowNull]
        public string? Waybill { get; set; }

        #region NAVIGATION FIELDS
        private User _winner;

        public User Winner
        {
            get => _loader.Load(this, ref _winner);
            set => _winner = value;
        }
        #endregion
    }
}
