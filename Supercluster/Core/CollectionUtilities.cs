﻿namespace Supercluster
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
                        maxIndex = i;
                    }

                    i++;
                }

                return maxIndex;
            }
        }

        /// <summary>
        /// Invokes a transform function on each element of <paramref name="source"/> and returns the index of the maximum <typeparamref name="TResult"/> value.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements in <paramref name="source"/>.</typeparam>
        /// <typeparam name="TResult">The type of the result of the <paramref name="selector"/>.</typeparam>
        /// <param name="source">A sequence of values to apply the transform <paramref name="selector"/> to.</param>
        /// <param name="selector">A transform to apply to each element in <paramref name="source"/>.</param>
        /// <returns>The index of the maximum <typeparamref name="TResult"/> value.</returns>
        public static int MaxIndex<TSource, TResult>(this IEnumerable<TSource> source, Func<TSource, TResult> selector)
        {
            var comparer = Comparer<TResult>.Default;
            using (var enumerator = source.GetEnumerator())
            {
                enumerator.MoveNext();
                var maxElement = selector(enumerator.Current);
                var maxIndex = 0;

                var i = 1;
                while (enumerator.MoveNext())
                {
                    if (comparer.Compare(selector(enumerator.Current), maxElement) > 0)
                    {
                        maxElement = selector(enumerator.Current);
                        maxIndex = i;
                    }

                    i++;
                }

                return maxIndex;
            }
        }

        /// <summary>
        /// Returns the indexes of the elements in <paramref name="source"/> satisfying the predicate.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements in <paramref name="source"/>.</typeparam>
        /// <param name="source">A sequence of values to filter based on <paramref name="predicate"/>.</param>
        /// <param name="predicate">The predicate use to filter values in <paramref name="source"/>.</param>
        /// <returns>The indexes of the elements in <paramref name="source"/> that satisfy <paramref name="predicate"/>.</returns>
        public static IEnumerable<int> WhereIndex<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate)
        {
            var indexes = new List<int>();
            using (var enumerator = source.GetEnumerator())
            {
                var i = 0;
                while (enumerator.MoveNext())
                {
                    if (predicate(enumerator.Current))
                    {
                        indexes.Add(i);
                    }

                    i++;
                }

                return indexes;
            }
        }

        /// <summary>
        /// Returns the index of the first element of a sequence that satisfies the given predicate.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements in <paramref name="source"/>.</typeparam>
        /// <param name="source">A sequence of values to find the first element satisfying the <paramref name="predicate"/>.</param>
        /// <param name="predicate">A predicate to test elements in <paramref name="source"/>.</param>
        /// <returns>The index of the first element in <paramref name="source"/> that satisfy <paramref name="predicate"/>.</returns>
        public static int FirstIndex<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate)
        {
            using (var enumerator = source.GetEnumerator())
            {
                var i = 0;
                while (enumerator.MoveNext())
                {
                    if (predicate(enumerator.Current))
                    {
                        return i;
                    }

                    i++;
                }
            }

            throw new InvalidOperationException($"No element satisfies the condition in {nameof(predicate)}.");
        }

        /// <summary>
        /// Returns the index of the first element of a sequence that satisfies the given predicate.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements in <paramref name="source"/>.</typeparam>
        /// <param name="source">A sequence of values to find the first element satisfying the <paramref name="predicate"/>.</param>
        /// <param name="predicate">A predicate to test elements in <paramref name="source"/>.</param>
        /// <returns>The index of the first element in <paramref name="source"/> that satisfy <paramref name="predicate"/>.
        /// If no element satisfies the <paramref name="predicate"/> then -1 is returned.</returns>
        public static int FirstIndexOrDefault<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate)
        {
            using (var enumerator = source.GetEnumerator())
            {
                var i = 0;
                while (enumerator.MoveNext())
                {
                    if (predicate(enumerator.Current))
                    {
                        return i;
                    }

                    i++;
                }
            }

            return -1;
        }

        /// <summary>
        /// Returns the index of the last element of a sequence that satisfies the given predicate.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements in <paramref name="source"/>.</typeparam>
        /// <param name="source">A sequence of values to find the last element satisfying the <paramref name="predicate"/>.</param>
        /// <param name="predicate">A predicate to test elements in <paramref name="source"/>.</param>
        /// <returns>The index of the last element in <paramref name="source"/> that satisfy <paramref name="predicate"/>.</returns>
        public static int LastIndex<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate)
        {
            var lastIndex = -1;
            using (var enumerator = source.GetEnumerator())
            {
                var i = 0;
                while (enumerator.MoveNext())
                {
                    if (predicate(enumerator.Current))
                    {
                        lastIndex = i;
                    }

                    i++;
                }
            }

            if (lastIndex == -1)
            {
                throw new InvalidOperationException($"No element satisfies the condition in {nameof(predicate)}.");
            }

            return lastIndex;
        }

        /// <summary>
        /// Returns the index of the last element of a sequence that satisfies the given predicate.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements in <paramref name="source"/>.</typeparam>
        /// <param name="source">A sequence of values to find the last element satisfying the <paramref name="predicate"/>.</param>
        /// <param name="predicate">A predicate to test elements in <paramref name="source"/>.</param>
        /// <returns>The index of the last element in <paramref name="source"/> that satisfy <paramref name="predicate"/>.
        /// If no element satisfies the <paramref name="predicate"/> then -1 is returned.</returns>
        public static int LastIndexOrDefault<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate)
        {
            var lastIndex = -1;
            using (var enumerator = source.GetEnumerator())
            {
                var i = 0;
                while (enumerator.MoveNext())
                {
                    if (predicate(enumerator.Current))
                    {
                        lastIndex = i;
                    }

                    i++;
                }
            }

            return lastIndex;
        }

        /// <summary>
        /// Returns all elements of a sequence whose index is in the provided sequence of indexes.
        /// Note: If an index is greater than the number of elements in <paramref name="source"/> it is ignored.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements in <paramref name="source"/>.</typeparam>
        /// <param name="source">A sequence of values to extract elements from.</param>
        /// <param name="indexes">The indexes </param>
        /// <returns>All elements in <paramref name="source"/> whose index is in <paramref name="indexes"/>.</returns>
        public static IEnumerable<TSource> WithIndexes<TSource>(this IEnumerable<TSource> source, IEnumerable<int> indexes)
        {
            return indexes.Select(source.ElementAt);
        }

        #endregion

        /// <summary>
        /// Gets the index of the elements returned by the <c>IEnumerable.Max()</c> function. 
        /// If there is more than one maximal element then the index return is chosen at random.
        /// </summary>
        /// <param name="array">The input array</param>
        /// <returns>The index of the max element in the array.</returns>
        public static int MaxIndexRandomTies(this double[] array)
        {
            var largestElement = double.MinValue;
            var largestElementIndex = -1;

            for (var i = 0; i < array.Length; i++)
            {
                if (largestElement < array[i])
                {
                    largestElement = array[i];
                    largestElementIndex = i;
                }
            }

            // we have more than one maximal element.Choose a random index
            if (array.Count(x => x == largestElement) > 1)
            {
                var indexes = array.WhereIndex(x => x == largestElement).ToArray();
                var rand = new Random();
                var randomIndex = rand.Next(0, indexes.Length);

                return indexes[randomIndex