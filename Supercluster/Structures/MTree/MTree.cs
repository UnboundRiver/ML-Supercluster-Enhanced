
﻿namespace Supercluster.Structures.MTree
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Supercluster.MTree;
    using Supercluster.MTree.NewDesign;
    using Supercluster.Structures;
    using Supercluster.Structures.Interfaces;
    using Supercluster.Structures.KDTree;

    /// <summary>
    ///
    /// </summary>
    /// <remarks>
    /// 
    /// MTrees have a number of properties that make them very attractive for use with large high dimensional data sets.
    /// 
    /// <list>
    /// They are self balancing.
    /// They do not require the items they index have a coordinate representation such as the <see cref="KDTree"/>.
    /// They handle high-dimensional data quite well as they index new items using the relative distance to other items already in the tree.
    /// This does not mean that MTrees are immune to the curse of dimensionality but they can efficiently handled data with 1000's of dimensions.
    /// 
    /// Draw Backs:
    /// The MTree makes explicit use of properties of Metric Spaces. If your data set cannot be appropriately embedded into a metric space you cannot use an MTree.
    /// For low dimension data sets with a coordinate representation, an MTree will be much slower than a KDTree (but still faster than a linear search).
    /// 
    /// MTrees are not easy to implement and their heavy reliance on Metric Space properties makes them quite difficult (if even possible) to modify for a non-metric space application.
    /// 
    /// </list>
    /// 
    /// References:
    /// [1] P. Ciaccia, M. Patella, and P. Zezula. M-tree: an efficient access method for similarity search in metric spaces. 
    /// In Proceedings of the 23rd International Conference on Very Large Data Bases (VLDB), pages 426–435, Athens, Greece, August 1997
    /// 
    /// [2] Samet, H., Foundations of Multidimensional and Metric Data Structures 1st Ed., Elsevier/Morgan Kaufmann, 2006
    /// </remarks>
    /// <seealso cref="KDTree{TNode}"/>
    /// <typeparam name="T"></typeparam>
    public class MTree<T> : ISpatialQueryable<T>
    {
        #region ISpatialQuerable Implementation

        /// <inheritdoc />
        public T this[int index] => this.internalArray[index];

        /// <inheritdoc />
        public IEnumerable<T> this[IEnumerable<int> indexes] => this.internalArray.WithIndexes(indexes);

        public IEnumerable<int> Add(IEnumerable<T> items)
        {
            return items.Select(this.Add);
        }

        /// <inheritdoc />
        public int Add(T item)
        {
            this.internalArray.Add(item);
            this.Add(this.Root, new MNodeEntry<int> { Value = this.internalArray.Count - 1 });
            return this.internalArray.Count - 1;
        }

        /// <inheritdoc />
        public IEnumerable<T> RadialSearch(T center, double radius) => this[this.RadialSearchIndexes(center, radius)];

        /// <inheritdoc />
        public IEnumerable<int> RadialSearchIndexes(T center, double radius)
        {
            var indexes = new List<int>();
            this.RadialSearch(this.Root, center, radius, indexes);
            return indexes;
        }

        /// <inheritdoc />
        public IEnumerable<T> NearestNeighbors(T target, int k) => this[this.NearestNeighborIndexes(target, k)];

        /// <inheritdoc />
        public IEnumerable<int> NearestNeighborIndexes(T target, int k)
        {
            var priorityQueue = new BoundablePriorityList<MNode<int>, double>(-1, false) { { this.Root, 0 } };
            var nearestNeighboorList = new BoundablePriorityList<MNodeEntry<int>, double>(k)
                                           {
                                                   { default(MNodeEntry<int>), double.PositiveInfinity }
                                           };

            while (priorityQueue.Any())
            {
                var nextNode = priorityQueue.MinElement;
                priorityQueue.RemoveAt(priorityQueue.Count - 1);
                this.KnnNodeSearch(nextNode, target, nearestNeighboorList, priorityQueue);
            }

            return nearestNeighboorList.Select(m => m.Value);
        }

        #endregion

        /// <summary>
        /// The internal array storing the actual points in the tree.
        /// </summary>
        private List<T> internalArray = new List<T>();

        /// <summary>
        /// Initializes a new instance of the <see cref="MTree{T}"/> class.
        /// An MTree is a type search tree supporting range and nearest neighbor searches.
        /// An <see cref="MTree{T}"/> is self balancing so data may be added after construction.
        /// An <see cref="MTree{T}"/> is faster than a <see cref="Supercluster.Structures.KDTree"/>
        /// for very high-dimensional data. Also, each point in an <see cref="MTree{T}"/> does not have
        /// to have an explicit coordinate representation. The spatial relationship between points is determined
        /// completely by the provided <paramref name="metric"/>.
        /// </summary>
        /// <param name="metric">A distance function satisfying the triangle inequality.</param>
        /// <param name="capacity">The capacity of each node in the tree.</param>
        /// <param name="data"></param>
        public MTree(Func<T, T, double> metric, int capacity, IEnumerable<T> data)
        {
            this.Metric = metric;
            this.Root = new MNode<int> { ParentEntry = null, Capacity = capacity };
            this.Capacity = capacity;

            foreach (var element in data)
            {
                this.Add(element);
            }
        }

        /// <summary>
        /// The capacity of each node in the tree. Varying the capacity can some times decrease search time.
        /// The exact relationship depends on the spatial / statistical properties of your data set.
        /// </summary>
        public int Capacity { get; }

        /// <summary>
        /// The root node of the MTree.
        /// </summary>
        public MNode<int> Root { get; private set; }
