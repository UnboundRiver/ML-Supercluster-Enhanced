namespace Supercluster
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// A utility class containing various extension methods to be used with the .NET collection classes.
    /// </summary>
    public static class CollectionUtilities
    {
        #region LinqToIndexes

        /// <summary>
        /// Returns the minimum value in a sequence of <typeparamref name="TSource"/> values using
        /// the default <see cref="Comparer{TSource}"/>.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements in <paramref name="source"/>.</typeparam>
        /// <param name="source">A sequence of values to determine the index of the minimum value.</param>
        /// <returns>The index of the minimum value in <paramref name="source"/>.</returns>
        public static int MinIndex<TSource>(this IEnumerable<TSource> source)
        {
            var comparer = Comparer<TSource>.Default;
            using (var enumerator = source.GetEnumerator())
            {
                enumerator.MoveNext();
                var minElement = enumerator.Current;
                var minIndex = 0;

                var i = 1;
                while (enumerator.MoveNext())
                {
                    if (comparer.Compare(enumerator.Current, minElement) < 0)
                    {
                        minElement = enumerator.Current;
                        minIndex = i;
                    }

                    i++;
                }

                return minIndex;
            }
        }

        /// <summary>
        /// Invokes a transform function on each element of <paramref name="source"/> and returns the 