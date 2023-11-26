namespace BidMasterOnline.Application.DTO
{
    /// <summary>
    /// DTO for setting new bid. (REQUEST)
    /// </summary>
    public class SetBidDTO
    {
        public Guid AuctionId { get; set; }
        public Guid UserId { get; set; }
        public decimal Amount { get; set; }
    }
}
