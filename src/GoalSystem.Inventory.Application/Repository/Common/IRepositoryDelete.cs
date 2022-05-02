using System.Threading.Tasks;

namespace GoalSystem.Inventory.Application.Repository.Common
{
    /// <summary>
    /// Generic interface to Delete objects
    /// </summary>
    /// <typeparam name="T">Generic Type</typeparam>
    public interface IRepositoryDelete<T>
    {
        /// <summary>
        /// Delete item to repository
        /// </summary>
        /// <param name="code">code of item to remove</param>
        /// <returns>Must be asynchronous -> Task (bool) if the operation is OK</returns>
        /// <summary>
        Task<bool> Delete(string code);
    }
}
