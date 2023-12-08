namespace BidMasterOnline.Application.Sessions
{
    public class SessionContext
    {
        public Guid? UserId { get; set; }
        public string? UserEmail { get; set; }
        public string? UserRole { get; set; }
    }
}
