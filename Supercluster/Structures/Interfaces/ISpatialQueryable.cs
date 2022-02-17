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
        /// <param name="it