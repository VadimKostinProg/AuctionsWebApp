namespace BidMasterOnline.Application.DTO
{
    public class BidDTO
    {
        public Guid Id { get; set; }
        public Guid AuctionId { get; set; }
        public string AuctionName { get; set; }
        public Guid BidderId { get; set; }
        public string BidderUserName { get; set; }
        public DateTime DateAndTime { get; set; }
        public decimal Amount { get; set; }
    }
}
