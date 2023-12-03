using BidMasterOnline.Application.Enums;

namespace BidMasterOnline.Application.DTO
{
    /// <summary>
    /// DTO with detailed information of auction. (RESPONSE)
    /// </summary>
    public class AuctionDetailsDTO : AuctionDTO
    {
        public DateTime StartDateAndTime { get; set; }
        public string LotDescription { get; set; }
        public string FinishType { get; set; }
        public string FinishTypeDescription { get; set; }
        public AuctionStatus Status { get; set; }
        List<BidDTO> Bids { get; set; }
    }
}
