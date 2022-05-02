using System.Threading.Tasks;

namespace GoalSystem.Inventory.Application.Repository.Common
{
    /// <summary>
    /// Generic interface to Update objects
    /// </summary>
    /// <typeparam name="T">Generic Type</typeparam>
    public interface IRepositoryUpdate<T>
    {
        /// <summary>
        /// Update the Item of repository
        /// </summary>
        /// <param name="entity">Entity in order to update</param>
        /// <returns>Must be asynchronous -> Task</returns>
        Task Update(T entity);
    }
}
