namespace BidMasterOnline.Application.ServiceContracts
{
    /// <summary>
    /// Service for sending notifications.
    /// </summary>
    public interface INotificationsService
    {
        /// <summary>
        /// Sends the notifications to specified user with specified title and message.
        /// </summary>
        /// <param name="email">Email of recipient.</param>
        /// <param name="title">Title of the notification.</param>
        /// <param name="message">Message of the notification.</param>
        Task SendNotificationAsync(string email, string title, string message);
    }
}
