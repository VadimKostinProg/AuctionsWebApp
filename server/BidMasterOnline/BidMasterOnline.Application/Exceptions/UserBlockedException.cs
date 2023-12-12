namespace BidMasterOnline.Application.Exceptions
{
    public class UserBlockedException : Exception
    {
        public UserBlockedException(string message): base(message) { }
    }
}
