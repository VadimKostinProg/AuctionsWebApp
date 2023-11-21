namespace BidMasterOnline.Application.DTO
{
    public class AuthenticationDTO
    {
        public Guid UserId { get; set; }
        public string Role { get; set; }
        public string Token { get; set; }
    }
}
