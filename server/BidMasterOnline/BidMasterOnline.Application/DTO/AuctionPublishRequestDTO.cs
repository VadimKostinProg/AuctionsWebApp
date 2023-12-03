using BidMasterOnline.Application.Enums;
using System.ComponentModel.DataAnnotations;

namespace BidMasterOnline.Application.DTO
{
    /// <summary>
    /// DTO for publishing auction for verification. (REQUEST)
    /// </summary>
    public class AuctionPublishRequestDTO
    {
        [Required(ErrorMessage = "Lot name is required.")]
        public string Name { get; set; } = null!;

        [Required(ErrorMessage = "Category is required.")]
        public Guid CategoryId { get; set; }

        [Required(ErrorMessage = "Lot description is required.")]
        public string LotDescription { get; set; } = null!;

        [Required(ErrorMessage = "Auction finish type is required.")]
        public AuctionFinishType FinishType { get; set; }

        [Required(ErrorMessage = "Auction finish time is required.")]
        public DateTime FinishDateTime { get; set; }

        public TimeSpan? FinishTimeInterval { get; set; }

        [Required(ErrorMessage = "Start price is required.")]
        [Range(100, 10e9)]
        public decimal StartPrice { get; set; }
    }
}
