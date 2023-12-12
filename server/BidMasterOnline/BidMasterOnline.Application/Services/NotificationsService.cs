using BidMasterOnline.Application.ServiceContracts;

namespace BidMasterOnline.Application.Services
{
    internal class NotificationsService : INotificationsService
    {
        public Task SendNotificationAsync(string email, string title, string message)
        {
            throw new NotImplementedException();
        }
    }
}
