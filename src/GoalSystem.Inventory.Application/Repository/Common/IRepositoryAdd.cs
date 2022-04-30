using System.Threading.Tasks;

namespace GoalSystem.Inventory.Application.Repository.Common
{
    public interface IRepositoryAdd<T>
    {
        Task Add(T entity);
    }
}
