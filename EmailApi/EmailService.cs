// using System;
// using System.Net;
// using System.Net.Mail;
// using Microsoft.Extensions.Configuration;
// using Microsoft.Extensions.Logging;

// namespace EmailApi
// {
//     public class EmailService
//     {
//         private readonly IConfiguration _configuration;
//         private readonly ILogger<EmailService> _logger;

//         public EmailService(IConfiguration configuration, ILogger<EmailService> logger)
//         {
//             _configuration = configuration;
//             _logger = logger;
//         }

//         public void SendEmail(string toEmail, string subject, string message)
//         {
//             var smtpSettings = _configuration.GetSection("SmtpSettings").Get<SmtpSettings>();

//             try
//             {
//                 using (var client = new SmtpClient(smtpSettings.Server, smtpSettings.Port))
//                 {
//                     client.Credentials = new NetworkCredential(smtpSettings.Username, smtpSettings.Password);
//                     client.EnableSsl = true;

//                     var mailMessage = new MailMessage
//                     {
//                         From = new MailAddress(smtpSettings.SenderEmail, smtpSettings.SenderName),
//                         Subject = subject,
//                         Body = message,
//                         IsBodyHtml = true,
//                     };

//                     mailMessage.To.Add(toEmail);

//                     client.Send(mailMessage);
//                 }
//             }
//             catch (Exception ex)
//             {
//                 _logger.LogError(ex, "Error sending email to {Email}", toEmail);
//                 throw;
//             }
//         }
//     }

//     public class SmtpSettings
//     {
//         public string Server { get; set; }
//         public int Port { get; set; }
//         public string SenderName { get; set; }
//         public string SenderEmail { get; set; }
//         public string Username { get; set; }
//         public string Password { get; set; }
//     }
// }
//------------------------------------------------------------------------------
using System;
using System.Net;
using System.Net.Mail;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace EmailApi
{
    public class EmailService
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<EmailService> _logger;

        public EmailService(IConfiguration configuration, ILogger<EmailService> logger)
        {
            _configuration = configuration;
            _logger = logger;
        }

        public void SendEmail(string toEmail, string subject, string message)
        {
            var smtpSettings = _configuration.GetSection("SmtpSettings").Get<SmtpSettings>();

            try
            {
                using (var client = new SmtpClient(smtpSettings.Server, smtpSettings.Port))
                {
                    client.Credentials = new NetworkCredential(smtpSettings.Username, smtpSettings.Password);
                    client.EnableSsl = true;

                    var mailMessage = new MailMessage
                    {
                        From = new MailAddress(smtpSettings.SenderEmail, smtpSettings.SenderName),
                        Subject = subject,
                        Body = message,
                        IsBodyHtml = true,
                    };

                    mailMessage.To.Add(toEmail);

                    client.Send(mailMessage);
                }
            }
            catch (SmtpException smtpEx)
            {
                _logger.LogError(smtpEx, "SMTP error sending email to {Email}", toEmail);
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "General error sending email to {Email}", toEmail);
                throw;
            }
        }

    }

    public class SmtpSettings
    {
        public string Server { get; set; }
        public int Port { get; set; }
        public string SenderName { get; set; }
        public string SenderEmail { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
