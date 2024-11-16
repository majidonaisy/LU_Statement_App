using SendGrid.Helpers.Mail;
using System.Net.Mail;
using System.Net;
using StatementApplication.Data;
using Microsoft.Extensions.Options;
using Azure.Core;

namespace StatementApplication.Services
{
    public class EmailSender
    {
        private readonly EmailSettings _emailSettings;
        public EmailSender(IOptions<EmailSettings> emailSettings)
        {
            _emailSettings = emailSettings.Value;

        }
        public async Task SendEmailAsync(string email, string subject, string message)
        {

            var smtpClient = new SmtpClient("smtp.office365.com", 587)
            {
                Port = _emailSettings.SmtpPort,
                Credentials = new NetworkCredential("majid.onaisy@st.ul.edu.lb", "1En?$i8H"),
                EnableSsl = true,
            };

            //var mailMessage = new MailMessage(from: "majid.onaisy@st.ul.edu.lb", to: email, subject, message);

            var mailMessage = new MailMessage
            {
                From = new MailAddress("majid.onaisy@st.ul.edu.lb"),
                Subject = subject,
                Body = message,
                IsBodyHtml = true // Ensure that the content is interpreted as HTML
            };

            mailMessage.To.Add(email);

            try
            {
                await smtpClient.SendMailAsync(mailMessage);
            }
            catch (SmtpException ex)
            {
                // Log the exception
                Console.WriteLine($"SMTP Exception: {ex.Message}");
                throw;
            }
        }
    }
}
