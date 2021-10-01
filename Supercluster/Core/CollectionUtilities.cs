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
        /// Invokes a transform function on each element of <paramref name="source"/> and returns the index of the minimum <typeparamref name="TResult"/> value.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements in <paramref name="source"/>.</typeparam>
        /// <typeparam name="TResult">The type of the result of the <paramref name="selector"/>.</typeparam>
        /// <param name="source">A sequence of values to apply the transform <see cref="selector"/> to.</param>
        /// <param name="selector">A transform to apply to each element in <paramref name="source"/>.</param>
        /// <returns>The index of the minimum <typeparamref name="TResult"/> value.</returns>
        public static int MinIndex<TSource, TResult>(this IEnumerable<TSource> source, Func<TSource, TResult> selector)
        {
            var comparer = Comparer<TResult>.Default;
            using (var enumerator = source.GetEnumerator())
            {
                enumerator.MoveNext();
                var minElement = selector(enumerator.Current);
                var minIndex = 0;

                var i = 1;
                while (enumerator.MoveNext())
                {
                    if (comparer.Compare(selector(enumerator.Current), minElement) < 0)
                    {
                        minElement = selector(enumerator.Current);
                        minIndex = i;
                    }

                    i++;
                }

                return minIndex;
            }
        }

        /// <summary>
        /// Returns the maximum value in a sequence of <typeparamref name="TSource"/> values using
        /// the default <see cref="Comparer{TSource}"/>.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements in <paramref name="source"/>.</typeparam>
        /// <param name="source">A sequence of values to determine the index of the maximum value.</param>
        /// <returns>The index of the maximum value in <paramref name="source"/>.</returns>
        public static int MaxIndex<TSource>(this IEnumerable<TSource> source)
        {
            var comparer = Comparer<TSource>.Default;
            using (var enumerator = source.GetEnumerator())
            {
                enumerator.MoveNext();
                var maxElement = enumerator.Current;
                var maxIndex = 0;

                var i = 1;
                while (enumerator.MoveNext())
                {
                    if (comparer.Compare(enumerator.Current, maxElement) > 0)
                    {
                        maxElement = enumerator.Current;
                        maxIndex =