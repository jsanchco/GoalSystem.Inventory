using System;

namespace GoalSystem.Inventory.Domain.Exceptions
{
    /// <summary>
    /// Exception controlled
    /// </summary>
    public class BusinessException : Exception
    {
        /// <summary>
        /// Constructor base 
        /// </summary>
        public BusinessException()
        {

        }

        /// <summary>
        /// Constructor 
        /// </summary>
        /// <param name="message">Message which initialice the class exception</param>
        public BusinessException(string message) : base(message)
        {

        }
    }
}
