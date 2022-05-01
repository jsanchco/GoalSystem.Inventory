using GoalSystem.Inventory.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GoalSystem.Inventory.Domain.Ensure
{
    public static class Arguments
    {
        public static void NotNullOrEmpty(string value, string parameterName = "")
        {
            if (string.IsNullOrEmpty(value))
                throw new BusinessException($"The argument '{parameterName}' must not be null or empty");
        }

        public static void NotNull(string value, string parameterName = "")
        {
            if (value is null)
                throw new BusinessException($"The argument '{parameterName}' must not be null");
        }

        public static void InEnum<T>(string value, string parameterName = "")
        {
            if (!Enum.GetValues(typeof(T)).Cast<T>().Any(x => x.ToString().Equals(value, StringComparison.InvariantCultureIgnoreCase)))
                throw new BusinessException($"The value '{value}' is not in '{parameterName}'");
        }

        public static void InEnum<T>(int value, string parameterName = "")
        {
            if (!Enum.GetValues(typeof(T)).Cast<int>().Any(x => x == value))
                throw new BusinessException($"The value '{value}' is not in '{parameterName}'");
        }

        public static void IsLessThanZero(int value, string parameterName = "")
        {
            if (value < 0)
                throw new BusinessException($"The argument '{parameterName}' must greather or equal that zero");
        }

        public static void IsNotNull<T>(List<T> collection, string parameterName = "")
        {
            if (collection is null)
                throw new BusinessException($"The argument '{parameterName}' must not be null");
        }

        public static void IsNotNullOrEmpty<T>(List<T> collection, string parameterName = "")
        {
            if (collection is null)
                throw new BusinessException($"The argument '{parameterName}' must not be null");

            if (collection.Count == 0)
                throw new BusinessException($"The argument '{parameterName}' must not be empty");
        }
    }
}
