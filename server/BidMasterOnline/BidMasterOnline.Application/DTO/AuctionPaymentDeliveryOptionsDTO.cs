using BidMasterOnline.Domain.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace BidMasterOnline.Application.DTO
{
    /// <summary>
    /// DTO for payment and delivery options of auction. (RESPONSE)
    /// </summary>
    public class AuctionPaymentDeliveryOptionsDTO
    {
        public Guid Id { get; set; }

        public Guid AuctionId { get; set; }

        public Guid? WinnerId { get; set; }

        public string? WinnersUsername { get; set; }

        public bool ArePaymentOptionsSet { get; set; }

        public DateTime? PaymentOptionsSetDateTime { get; set; }

        public string? IBAN { get; set; }

        public bool AreDeliveryOptionsSet { get; set; }

        public DateTime? DeliveryOptionsSetDateTime { get; set; }

        public string? Country { get; set; }

        public string? City { get; set; }

        public bool IsPaymentConfirmed { get; set; }

        public DateTime? PaymentConfirmationDateTime { get; set; }

        public bool IsDeliveryConfirmed { get; set; }

        public DateTime? DeliveryConfirmationDateTime { get; set; }

        public string? Waybill { get; set; }
    }
}
