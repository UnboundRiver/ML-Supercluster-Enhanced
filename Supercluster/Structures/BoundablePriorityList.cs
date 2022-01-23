
﻿namespace Supercluster.Structures
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Runtime.CompilerServices;

    using Supercluster.Core;

    /// <summary>
    /// A list of limited length that remains sorted by <typeparamref name="TPriority"/>.
    /// Useful for keeping track of items in nearest neighbor searches. Insert is O(log n). Retrieval is O(1)
    /// </summary>
    /// <typeparam name="TElement">The type of element the list maintains.</typeparam>
    /// <typeparam name="TPriority">The type the elements are prioritized by.</typeparam>
    [DebuggerDisplay("Count = {Count}")]
    public class BoundablePriorityList<TElement, TPriority> : IEnumerable<TElement>
        where TPriority : IComparable<TPriority>
    {
        /// <summary>
        /// Used to reverse sort order when the list is not in ascending mode.
        /// </summary>
        private readonly ReverseComparer<TPriority> descendingComparer = new ReverseComparer<TPriority>();

        /// <summary>
        /// The list holding the actual elements
        /// </summary>
        private List<TElement> elementList;

        /// <summary>
        /// The list of priorities for each element.
        /// There is a one-to-one correspondence between the
        /// priority list ad the element list.
        /// </summary>
        private List<TPriority> priorityList;

        /// <summary>
        /// Gets the element with the largest priority.
        /// </summary>
        public TElement MaxElement => this.IsAscending ? this.elementList[this.Count - 1] : this.elementList[0];

        /// <summary>
        /// Gets the largest priority. currently in the list.
        /// </summary>
        public TPriority MaxPriority => this.IsAscending ? this.priorityList[this.Count - 1] : this.priorityList[0];

        /// <summary>
        /// Gets the element with the lowest priority.
        /// </summary>
        public TElement MinElement => this.IsAscending ? this.elementList[0] : this.elementList[this.Count - 1];

        /// <summary>
        /// Gets the smallest priority.
        /// </summary>
        public TPriority MinPriority => this.IsAscending ? this.priorityList[0] : this.priorityList[this.Count - 1];

        /// <summary>
        /// Gets the maximum allowed capacity for this list.
        /// If this instance does not have a max capacity set then the result is -1.
        /// </summary>
        public int Capacity { get; }

        /// <summary>
        /// Returns true if the list is at maximum capacity. This is always false if the list was not initialized with a capacity.
        /// </summary>
        public bool IsFull => this.Count == this.Capacity;

        /// <summary>
        /// Returns true if the list is sorted in ascending priority. Otherwise the return value is false
        /// and the list is sorted in descending priority.
        /// </summary>
        public bool IsAscending { get; }

        /// <summary>
        /// Returns the count of items currently in the list.
        /// </summary>
        public int Count => this.priorityList.Count;

        /// <summary>
        /// Indexer for the internal element array.
        /// </summary>
        /// <param name="index">The index in the array.</param>
        /// <returns>The element at the specified index.</returns>
        public TElement this[int index] => this.elementList[index];

        /// <summary>
        /// Initializes a new instance of the <see cref="BoundablePriorityList{TElement, TPriority}"/> class.