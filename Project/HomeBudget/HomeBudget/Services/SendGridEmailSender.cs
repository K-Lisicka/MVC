// Services/SendGridEmailSender.cs
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace HomeBudget.Services
{
    // Klasa, która wysyła e-maile przez SendGrid
    public class SendGridEmailSender : IEmailSender
    {
        private readonly SendGridClient _client;
        private readonly string _fromEmail;
        private readonly string _fromName;

        public SendGridEmailSender(IOptions<SendGridOptions> opt)
        {
            _client = new SendGridClient(opt.Value.ApiKey);
            _fromEmail = opt.Value.FromEmail;
            _fromName = opt.Value.FromName;
        }

        public async Task SendEmailAsync(string to, string subject, string html)
        {
            var msg = MailHelper.CreateSingleEmail(
                new EmailAddress(_fromEmail, _fromName),
                new EmailAddress(to),
                subject,
                plainTextContent: null,
                htmlContent: html);

            await _client.SendEmailAsync(msg);
        }
    }

    // Klasa używana do wczytywania ustawień z appsettings.json / User Secrets
    public record SendGridOptions
    {
        public string ApiKey { get; init; } = default!;
        public string FromEmail { get; init; } = default!;
        public string FromName { get; init; } = default!;
    }
}

