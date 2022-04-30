using System.Threading.Tasks;

namespace GoalSystem.Inventory.Application.Repository.Common
{
    public interface IRepositoryDelete<T>
    {
        Task Delete(string code);
    }
}
