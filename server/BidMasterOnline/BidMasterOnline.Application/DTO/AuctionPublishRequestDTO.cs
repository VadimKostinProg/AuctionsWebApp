using BidMasterOnline.Application.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BidMasterOnline.Application.DTO
{
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
        public DateTime? FinishDateTime { get; set; }
        public TimeSpan? FinishTimeInterval { get; set; }

        [Required(ErrorMessage = "Start price is required.")]
        [Range(100, 10e9)]
        public decimal StartPrice { get; set; }
    }
}
