using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;
using SolutionFramework.EmailServices.Configuration;
using SolutionFramework.EmailServices.IServices;

namespace SolutionFramework.EmailServices.Services
{
    public class SendGridEmailServices : ISendGridEmailServices
    {
        private readonly EmailSettings _settings;
        private readonly ILogger<SendGridEmailServices> _logger;

        public SendGridEmailServices(IOptions<EmailSettings> settings, ILogger<SendGridEmailServices> logger)
        {
            _settings = settings.Value;
            _logger = logger;
        }

        public async Task<bool> SendEmailAsync(Email email)
        {
            _logger.LogInformation($"-- Iniciando con el envío del Email. {email.To}");

            var client = new SendGridClient(_settings.ApiKey);
            var subject = email.Subject;
            var to = new EmailAddress(email.To);
            var emailBody = email.Body;
            var from = new EmailAddress
            {
                Email = _settings.FromAddress,
                Name = _settings.FromName
            };

            var sendMessage = MailHelper.CreateSingleEmail(from, to, subject, emailBody, emailBody);
            var response = await client.SendEmailAsync(sendMessage);

            if (response.StatusCode == System.Net.HttpStatusCode.Accepted
                || response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                _logger.LogInformation($"Mensaje enviado satisfactoriamente a: {email.To}");
                return true;
            }

            _logger.LogInformation($"Error al enviar el Mensaje a: {email.To}");
            return false;
        }
    }
}
