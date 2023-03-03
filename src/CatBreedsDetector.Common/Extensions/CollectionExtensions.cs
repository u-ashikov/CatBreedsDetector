namespace CatBreedsDetector.Common.Extensions
{
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// A static class containing useful extensions methods for collections.
    /// </summary>
    public static class CollectionExtensions
    {
        /// <summary>
        /// Use this method to check whether a collection is null or has no elements in it.
        /// </summary>
        /// <param name="collection">The collection that should be checked.</param>
        /// <typeparam name="T">The type of the elements within the collection.</typeparam>
        /// <returns>A boolean indicating whether the collection is null or has no elements in it.</returns>
        public static bool IsNullOrEmpty<T>(this IEnumerable<T> collection)
            => collection is null || !collection.Any();
    }
}