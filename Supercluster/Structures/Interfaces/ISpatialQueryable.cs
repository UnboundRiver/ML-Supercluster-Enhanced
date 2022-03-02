namespace Supercluster.Structures.Interfaces
{
    using System.Collections.Generic;

    /// <summary>
    /// An append only data structure that supports basic spatial access methods.
    /// </summary>
    /// <typeparam name="T">The type of the objects in the collection.</typeparam>
    public interface ISpatialQueryable<T>
    {
        /// <summary>
        /// Add the items to the current collection/
        /// </summary>
        /// <param name="items">The items to be added.</param>
        /// <returns>The indexes of the items added.</returns>
        IEnumerable<int> Add(IEnumerable<T> items);

        /// <summary>
        /// Adds the item to the current collection.
        /// </summary>
        /// <param name="item">The item to be added.</param>
        /// <returns>The index of the item added.</returns>
        int Add(T item);

        /// <summary>
        /// Searches for the <paramref name="k"/> nearest neighbors of the specified <paramref name="target"/>.
        /// Note that the target point may or may not be in the collection.
        /// </summary>
        /// <param name="target">The point whose neighbors will be found.</param>
        /// <param name="k">The number of neighbors find.</param>
        /// <returns>The <paramref name="k"/> nearest neighbors of the <paramref name="target"/> point.</returns>
        IEnumerable<T> NearestNeighbors(T target, int k);

        /// <summary>
        /// Searches for the <paramref name="k"/> nearest neighbors of the specified <paramref name