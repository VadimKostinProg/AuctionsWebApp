using BidMasterOnline.Application.Enums;

namespace BidMasterOnline.Application.DTO
{
    public class AuctionSpecifications : Specifications
    {
        public string? Category { get; set; }
        public decimal? MinStartPrice { get; set; }
        public decimal? MaxStartPrice { get; set; }
        public decimal? MinCurrentBid { get; set; }
        public decimal? MaxCurrentBid { get; set; }
        public AuctionStatus? Status { get; set; }
    }
}
