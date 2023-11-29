namespace BidMasterOnline.Application.DTO
{
    /// <summary>
    /// DTO with detailed information of auction. (RESPONSE)
    /// </summary>
    public class AuctionDetailedDTO : AuctionDTO
    {
        public DateTime StartDateAndTime { get; set; }
        public string LotDescription { get; set; }
        public string FinishType { get; set; }
        public string FinishTypeDescription { get; set; }
        List<BidDTO> Bids { get; set; }
    }
}
