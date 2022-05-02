using System.Collections.Generic;
using System.Threading.Tasks;

namespace GoalSystem.Inventory.Application.Repository.Common
{
    /// <summary>
    /// Generic interface to Read objects
    /// </summary>
    /// <typeparam name="T">Generic Type</typeparam>
    public interface IRepositoryRead<T>
    {
        /// <summary>
        /// Get all the Items
        /// </summary>
        /// <returns></returns>
        Task<List<T>> GetAll();

        /// <summary>
        /// Get the Item of repository by Id
        /// </summary>
        /// <param name="filterCode"></param>
        /// <returns>Must be asynchronous -> Task (Entity)</returns>
        Task<T> GetById(string filterCode);
    }
}
