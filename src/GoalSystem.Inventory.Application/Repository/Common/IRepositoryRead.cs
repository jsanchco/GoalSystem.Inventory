using System.Collections.Generic;
using System.Threading.Tasks;

namespace GoalSystem.Inventory.Application.Repository.Common
{
    public interface IRepositoryRead<T>
    {
        Task<List<T>> GetAll();
        Task<T> GetById(string filterCode);
    }
}
