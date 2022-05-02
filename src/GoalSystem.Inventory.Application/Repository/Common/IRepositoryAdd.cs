using System.Threading.Tasks;

namespace GoalSystem.Inventory.Application.Repository.Common
{
    /// <summary>
    /// Generic interface to Add objects
    /// </summary>
    /// <typeparam name="T">Generic Type</typeparam>
    public interface IRepositoryAdd<T>
    {
        /// <summary>
        /// Add item to repository
        /// </summary>
        /// <param name="entity">Generic entity in order to Add</param>
        /// <returns>Must be asynchronous -> Task</returns>
        Task Add(T entity);
    }
}
