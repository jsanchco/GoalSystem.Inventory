using GoalSystem.Inventory.Domain.Models;
using System.Threading.Tasks;

namespace GoalSystem.Inventory.Application.Services
{
    public interface ISendEmailService
    {
        Task<bool> Send(Email email);
    }
}
