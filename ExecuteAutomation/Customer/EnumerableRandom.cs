using System;
using System.Collections.Generic;
using System.Linq;

namespace ExecuteAutomation.Customer
{
    /// <summary>
    /// Helper to pick random item
    /// </summary>
    public static class EnumerableRandom
    {
        /// <summary>
        /// Pick a random item from the list and return it.
        /// </summary>
        /// <typeparam name="T">Type of object.</typeparam>
        /// <param name="source">List of objects.</param>
        /// <returns>Object returned.</returns>
        public static T Random<T>(this IEnumerable<T> source)
        {
            var rnd = new Random(Guid.NewGuid().GetHashCode());
            return source.OrderBy(item => rnd.Next()).First();
        }
    }
}
