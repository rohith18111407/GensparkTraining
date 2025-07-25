using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WareHouseFileArchiver.Interfaces;
using WareHouseFileArchiver.Models.Domains;
using System.Net;
using System.Net.Mail;
using System.Text.RegularExpressions;

namespace WareHouseFileArchiver.Services
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration configuration;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly ILogger<EmailService> logger;

        public EmailService(IConfiguration configuration, UserManager<ApplicationUser> userManager, ILogger<EmailService> logger)
        {
            this.configuration = configuration;
            this.userManager = userManager;
            this.logger = logger;
        }

        private bool IsValidEmail(string email)
        {
            return !string.IsNullOrWhiteSpace(email) &&
                   Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$");
        }

        public async Task SendMessageToAllUsersAsync(string subject, string message)
        {
            var users = await userManager.Users.ToListAsync();
            foreach (var user in users)
            {
                if (!IsValidEmail(user.Email))
                {
                    logger.LogWarning("Skipping invalid email: {Email}", user.Email);
                    continue;
                }

                try
                {
                    await SendEmailAsync(user.Email!, subject, message);
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "Failed to send email to {Email}", user.Email);
                }
            }
        }

        public async Task SendEmailAsync(string to, string subject, string body)
        {
            try
            {
                var smtp = configuration.GetSection("Email:Smtp");

                using var message = new MailMessage
                {
                    From = new MailAddress(smtp["From"]),
                    Subject = subject,
                    Body = body,
                    IsBodyHtml = false
                };

                message.To.Add(new MailAddress(to));

                using var smtpClient = new SmtpClient
                {
                    Host = smtp["Host"],
                    Port = int.Parse(smtp["Port"]),
                    EnableSsl = true,
                    Credentials = new NetworkCredential(smtp["Username"], smtp["Password"])
                };

                await smtpClient.SendMailAsync(message);
                logger.LogInformation("Email sent to {Email}", to);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error sending email to {Email}", to);
                throw;
            }
        }
    }
}
