
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

        /// <summary>
        /// The metric used for distance computations between points in the tree. This metric function
        /// must satisfy the triangle inequality or the search functions will return incorrect results.
        /// </summary>
        public Func<T, T, double> Metric { get; }

        /// <summary>
        /// Attempts to add the <see cref="MNodeEntry{TValue}"/> to the specified node in the tree. If the node cannot be added the tree will create new nodes and rebalance.
        /// </summary>
        /// <param name="node">The node to add the new entry to.</param>
        /// <param name="newNodeEntry">The new node entry to add.</param>
        private void Add(MNode<int> node, MNodeEntry<int> newNodeEntry)
        {
            /*
                NOTE: The insertion, split, partition and promotion methods are quite complicated. If you are trying to understand this code you should
                really consider reading the original paper:
                P. Ciaccia, M. Patella, and P. Zezula. M-tree: an efficient access method for similarity search in metric spaces.
            */

            // If we are trying to insert into an internal node then we determine if the new entry resides in the ball of any
            // entry in the current node. If it does reside in a ball, then we recurse in to that entry's child node.
            // If the new entry does NOT reside in any of the balls then we picks the entry whose ball should expand the least to enclose
            // the new node entry.
            if (node.IsInternalNode)
            {
                // Anonymous type to store the entries and their distance from the new node entry.
                var entriesWithDistance = node.Entries.Select(n => new { Node = n, Distance = this.Metric(this[n.Value], this[newNodeEntry.Value]) }).ToArray();

                // This would be all the entries
                var ballsContainingEntry = entriesWithDistance.Where(d => d.Distance < d.Node.CoveringRadius).ToArray();
                MNodeEntry<int> closestEntry;

                if (ballsContainingEntry.Any())
                {
                    // New entry is currently in the region of a ball
                    closestEntry = ballsContainingEntry[ballsContainingEntry.Select(b => b.Distance).MinIndex()].Node;
                }
                else
                {
                    // The new element does not currently reside in any of the current regions balls.
                    // Since we are not in any of the balls we find which whose radius we must increase the least
                    var closestEntryIndex = entriesWithDistance.Select(d => d.Distance - d.Node.CoveringRadius).MinIndex();
                    closestEntry = entriesWithDistance[closestEntryIndex].Node;
                    closestEntry.CoveringRadius = entriesWithDistance[closestEntryIndex].Distance;
                }

                // Recurse into the closest elements subtree
                this.Add(closestEntry.ChildNode, newNodeEntry);
            }
            else
            {
                if (!node.IsFull)
                {
                    // Node is a leaf node. If the node is not full we simply add to that node.
                    if (node == this.Root)
                    {
                        node.Add(newNodeEntry);
                    }
                    else
                    {
                        newNodeEntry.DistanceFromParent = this.Metric(this.internalArray[node.ParentEntry.Value], this.internalArray[newNodeEntry.Value]);
                        node.Add(newNodeEntry);
                    }
                }
                else
                {
                    this.Split(node, newNodeEntry);
                }
            }

        }

        /// <summary>
        /// Splits a leaf node and adds the <paramref name="newEntry"/>
        /// </summary>
        /// <param name="node"></param>
        /// <param name="newEntry"></param>
        private void Split(MNode<int> node, MNodeEntry<int> newEntry)
        {
            var nodeIsRoot = node == this.Root;
            MNode<int> parent = null;
            var parentEntryIndex = -1;

            if (!nodeIsRoot)
            {
                // keep reference to parent node
                parent = node.ParentEntry.EnclosingNode;
                parentEntryIndex = parent.Entries.IndexOf(node.ParentEntry);
                //if we are not the root, the get the parent of the current node.
            }

            // Create local copy of entries
            var entries = node.Entries.ToList();
            entries.Add(newEntry);

            var newNode = new MNode<int> { Capacity = this.Capacity };
            var promotionResult = this.Promote(entries.ToArray(), node.IsInternalNode);

            // TODO: Does not need to be an array
            node.Entries = promotionResult.FirstPartition;
            newNode.Entries = promotionResult.SecondPartition;

            // Set child nodes of promotion objects
            promotionResult.FirstPromotionObject.ChildNode = node;
            promotionResult.SecondPromotionObject.ChildNode = newNode;

            if (nodeIsRoot)
            {
                // if we are the root node, then create a new root and assign the promoted objects to them
                var newRoot = new MNode<int> { ParentEntry = null, Capacity = this.Capacity };
                newRoot.AddRange(
                    new List<MNodeEntry<int>>
                        {
                                promotionResult.FirstPromotionObject,
                                promotionResult.SecondPromotionObject
                        });

                this.Root = newRoot;
            }
            else // we are not the root
            {
                // Set distance from parent
                if (parent == this.Root)
                {
                    promotionResult.FirstPromotionObject.DistanceFromParent = -1;
                }
                else
                {
                    promotionResult.FirstPromotionObject.DistanceFromParent =
                        this.Metric(this.internalArray[promotionResult.FirstPromotionObject.Value], this.internalArray[parent.ParentEntry.Value]);
                }
