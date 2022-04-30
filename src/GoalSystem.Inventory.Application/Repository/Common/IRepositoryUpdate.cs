using System.Threading.Tasks;

namespace GoalSystem.Inventory.Application.Repository.Common
{
    public interface IRepositoryUpdate<T>
    {
        Task Update(T entity);
    }
}
