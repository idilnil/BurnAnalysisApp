using System.Net.Mail;
using System.Net;
using Microsoft.Extensions.Configuration;

namespace BurnAnalysisApp.Services
{
    public interface IEmailService
    {
        Task SendVerificationEmailAsync(string doctorEmail, string doctorName);
    }

    public class EmailService : IEmailService
    {
        private readonly IConfiguration _configuration;

        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task SendVerificationEmailAsync(string doctorEmail, string doctorName)
        {
            try
            {
                var smtpSettings = _configuration.GetSection("SmtpSettings");
                var smtpClient = new SmtpClient(smtpSettings["Host"])
                {
                    Port = int.Parse(smtpSettings["Port"]),
                    Credentials = new NetworkCredential(smtpSettings["Username"], smtpSettings["Password"]),
                    EnableSsl = true,
                };

                var mailMessage = new MailMessage
                {
                    From = new MailAddress(smtpSettings["Username"]),
                    Subject = "Hesabınız Onaylandı",
                    Body = $"Sayın Dr. {doctorName},\n\nHesabınız admin tarafından onaylanmıştır. Artık sisteme giriş yapabilirsiniz.\n\nSaygılarımızla,\nBurn Analysis App",
                    IsBodyHtml = false
                };
                mailMessage.To.Add(doctorEmail);

                await smtpClient.SendMailAsync(mailMessage);
                Console.WriteLine($"Email successfully sent to {doctorEmail}");
            }
            catch (Exception ex)
            {
                // Hata loglama
                Console.WriteLine($"Error sending verification email: {ex.Message}");
                throw;  // Hata tekrar fırlatılabilir
            }
        }

    }
}