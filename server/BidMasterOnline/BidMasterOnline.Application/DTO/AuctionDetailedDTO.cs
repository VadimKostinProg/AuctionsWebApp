namespace BidMasterOnline.Application.DTO
{
    public class AuctionDetailedDTO : AuctionDTO
    {
        public DateTime StartDateAndTime { get; set; }
        public string LotDescription { get; set; }
        public string FinishType { get; set; }
        public string FinishTypeDescription { get; set; }
        List<BidDTO> Bids { get; set; }
    }
}
