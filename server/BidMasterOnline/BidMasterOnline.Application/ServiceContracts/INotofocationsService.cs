namespace BidMasterOnline.Application.ServiceContracts
{
    /// <summary>
    /// Service for sending notofocations.
    /// </summary>
    public interface INotofocationsService
    {
        /// <summary>
        /// Sends the notifications to specified user with specified title and message.
        /// </summary>
        /// <param name="email">Email of recipient.</param>
        /// <param name="title">Title of the notification.</param>
        /// <param name="message">Message of the notification.</param>
        Task SendNotification(string email, string title, string message);
    }
}
