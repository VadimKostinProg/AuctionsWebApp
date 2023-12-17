using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace BidMasterOnline.Domain.Entities
{
    public class AuctionPaymentDeliveryOptions : EntityBase
    {
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

        public bool IsPaymentConfirmed { get; set; }

        [AllowNull]
        public DateTime? PaymentConfirmationDateTime { get; set; }

        public bool IsDeliveryConfirmed { get; set; }

        [AllowNull]
        public DateTime? DeliveryConfirmationDateTime { get; set; }

        [AllowNull]
        public string? Waybill { get; set; }

        [ForeignKey(nameof(WinnerId))]  
        public virtual User Winner { get; set; }
    }
}
