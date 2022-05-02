using GoalSystem.Inventory.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GoalSystem.Inventory.Domain.Ensure
{
    /// <summary>
    /// Static class that valid certains characteristics of objects
    /// </summary>
    public static class Arguments
    {
        /// <summary>
        /// Valid if one string is Null or Empty and throw exception controlled 
        /// </summary>
        /// <param name="value">Value of string</param>
        /// <param name="parameterName">Name of object</param>
        public static void NotNullOrEmpty(string value, string parameterName = "")
        {
            if (string.IsNullOrEmpty(value))
                throw new BusinessException($"The argument '{parameterName}' must not be null or empty");
        }

        /// <summary>
        /// Valid if one string is Null and throw exception controlled 
        /// </summary>
        /// <param name="value">Value of string</param>
        /// <param name="parameterName">Name of object</param>
        public static void NotNull(string value, string parameterName = "")
        {
            if (value is null)
                throw new BusinessException($"The argument '{parameterName}' must not be null");
        }

        /// <summary>
        /// Valid if one string is in Enum and throw exception controlled
        /// </summary>
        /// <typeparam name="T">Generic type</typeparam>
        /// <param name="value">value of eumeration</param>
        /// <param name="parameterName">name of Enum</param>
        public static void InEnum<T>(string value, string parameterName = "")
        {
            if (!Enum.GetValues(typeof(T)).Cast<T>().Any(x => x.ToString().Equals(value, StringComparison.InvariantCultureIgnoreCase)))
                throw new BusinessException($"The value '{value}' is not in '{parameterName}'");
        }

        /// <summary>
        /// Valid if one int is inside of enum and throw exception controlled
        /// </summary>
        /// <typeparam name="T">Generic type</typeparam>
        /// <param name="value">value int of enum</param>
        /// <param name="parameterName">name of Enum</param>
        public static void InEnum<T>(int value, string parameterName = "")
        {
            if (!Enum.GetValues(typeof(T)).Cast<int>().Any(x => x == value))
                throw new BusinessException($"The value '{value}' is not in '{parameterName}'");
        }

        /// <summary>
        /// Valid if one int is less than zero and throw exception controlled
        /// </summary>
        /// <param name="value">value of int</param>
        /// <param name="parameterName">name of int object</param>
        public static void IsLessThanZero(int value, string parameterName = "")
        {
            if (value < 0)
                throw new BusinessException($"The argument '{parameterName}' must greather or equal that zero");
        }

        /// <summary>
        /// Valid if one collection in not null or empty and throw exception controlled
        /// </summary>
        /// <typeparam name="T">Generic type</typeparam>
        /// <param name="collection">collection in order to evaluate</param>
        /// <param name="parameterName">name of collection</param>
        public static void IsNotNull<T>(List<T> collection, string parameterName = "")
        {
            if (collection is null)
                throw new BusinessException($"The argument '{parameterName}' must not be null");
        }


        /// <summary>
        /// Valid if one collection in not null and throw exception controlled
        /// </summary>
        /// <typeparam name="T">Generic type</typeparam>
        /// <param name="collection">collection in order to evaluate</param>
        /// <param name="parameterName">name of collection</param>
        public static void IsNotNullOrEmpty<T>(List<T> collection, string parameterName = "")
        {
            if (collection is null)
                throw new BusinessException($"The argument '{parameterName}' must not be null");

            if (collection.Count == 0)
                throw new BusinessException($"The argument '{parameterName}' must not be empty");
        }
    }
}
