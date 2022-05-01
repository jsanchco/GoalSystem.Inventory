using GoalSystem.Inventory.Application.Services;
using GoalSystem.Inventory.Domain.Models;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace GoalSystem.Inventory.Infrastructure.Services
{
    public class SendEmailFakeService : ISendEmailService
    {
        private readonly ILogger<SendEmailFakeService> _logger;

        public SendEmailFakeService(ILogger<SendEmailFakeService> logger)
        {
            _logger = logger;
        }

        public async Task<bool> Send(Email email)
        {
            _logger.LogInformation($"SendEmail Fake to [{email.To}], with Subject [{email.Subject}] and Body [{email.Body}]");
            
            return await Task.FromResult(true);
        }
    }
}
