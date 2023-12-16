using BidMasterOnline.Application.ServiceContracts;
using Microsoft.Extensions.Configuration;
using System.Net.Mail;
using System.Net;

namespace BidMasterOnline.Application.Services
{
    public class NotificationsService : INotificationsService
    {
        private readonly IConfiguration _configuration;

        public NotificationsService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task SendNotificationAsync(string email, string subject, string message)
        {
            string senderEmail = _configuration["TechnicalSupportTeamCredentials:Email"]!;
            string senderPassword = _configuration["TechnicalSupportTeamCredentials:Password"]!;

            var client = new SmtpClient("smtp.office365.com", 587)
            {
                EnableSsl = true,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(senderEmail, senderPassword)
            };

            var mailMessage = new MailMessage(from: senderEmail, to: email, subject, message);

            await client.SendMailAsync(mailMessage);
        }
    }
}
