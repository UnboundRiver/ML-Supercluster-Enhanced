// <copyright file="KDTree.cs" company="Eric Regina">
// Copyright (c) Eric Regina. All rights reserved.
// </copyright>

namespace Supercluster.Structures.KDTree
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Supercluster.Core;
    using Supercluster.DataStructures.KDTree;
    using Supercluster.Structures;
    using Supercluster.Structures.Interfaces;

    public class CoordinateWithIndex
    {
        public double[] Coordinate { get; }

        public int Index { get; }

        public CoordinateWithIndex(int index, double[] Coordinate)
        {
            this.Index = index;
            this.Coordinate = Coordinate;
        }
    }

    /// <summary>
    /// The node of a KDTree.
    /// </summary>
    public class KDNode
    {
        /// <summary>
        /// The index of the element in the internal array.
        /// </summary>
        public int ElementIndex { get; set; }

        /// <summary>
        /// The left child of the node.
        /// </summary>
        public KDNode Left;

        /// <summary>
        /// The right child of of the node;
        /// </summary>
        public KDNode Right;

        /// <summary>
        /// The parent of the current node. If the current node is the root node, then its parent is null.
        /// </summary>
        public KDNode Parent;

        public KDNode(int index, KDNode parent = null, KDNode left = null, KDNode right = null)
        {
            this.ElementIndex = index;
            this.Left = left;
            this.Right = right;
            this.Parent = parent;
        }
    }

    /// <summary>
    /// Represents a KD-Tree. KD-Trees are used for fast spatial searches. Searching in a
    /// balanced KD-Tree is O(log n) where linear search is O(n). Points in the KD-Tree are
    /// equi-length arrays of type <typeparamref name="double"/>. The node objects associated
    /// with the points is an array of type <typeparamref name="TNode"/>.
    /// </summary>
    /// <remarks>
    /// KDTrees can be fairly difficult to understand at first. The following references helped me
    /// understand what exactly a KDTree is doing and the contain the best descriptions of searches in a KDTree.
    /// Samet's book is the best reference of multidimensional data structures I have ever seen. Wikipedia is also a good starting place.
    /// References:
    /// <ul style="list-style-type:none">
    /// <li> <a href="http://store.elsevier.com/product.jsp?isbn=9780123694461">Foundations of Multidimensional and Metric Data Structures, 1st Edition, by Hanan Samet. ISBN: 9780123694461</a> </li>
    /// <li> <a href="https://en.wikipedia.org/wiki/K-d_tree"> https://en.wikipedia.org/wiki/K-d_tree</a> </li>
    /// </ul>
    /// </remarks>
    [Serializable]
    public class KDTree<TNode> : ISpatialQueryable<TNode>
    {
        /// <summary>
        /// The number of points in the KDTree
        /// </summary>
        public int Count => this.InternalList.Count;

        /// <summary>
        /// The numbers of dimensions that the tree has.
        /// </summary>
        public int Dimensions { get; }

        /// <summary>
        /// The metric function used to calculate distance between points.
        /// </summary>
        public Func<double[], double[], double> Metric { get; set; }

        private List<TNode> InternalList { get; }

        public KDTreeNavigator<TNode> Root => new KDTreeNavigator<TNode>(this, this.rootNode);

        private KDNode rootNode = new KDNode(-1);

        /// <summary>
        /// Initializes a new instance of the <see cref="KDTree{TNode}"/> class.
        /// </summary>
        /// <param name="dimensions">The number of dimensions in the data set.</param>
        /// <param name="nodes">The nodes associated with each point.</param>
        /// <param name="metric">The metric function which implicitly defines the metric space in which the KDTree operates in. This should satisfy the triangle inequality.</param>
        public KDTree(
            int dimensions,
            Func<double[], double[], double> metric,
            Func<TNode, double[]> selector = null)
        {
            this.PointSelector = selector ?? CoordinateSelector.CreateDefaultCoordinateSelector<TNode>();
            this.InternalList = new List<TNode>();
            this.Dimensions = dimensions;
            this.Metric = metric;
        }

        public KDTree(int dimensions,
            IEnumerable<TNode> nodes,
            Func<double[], double[], double> metric,
            Func<TNode, double[]> selector = null)
        {
            this.PointSelector = selector ?? CoordinateSelector.CreateDefaultCoordinateSelector<TNode>();
            this.InternalList = new List<TNode>(nodes);
            this.Dimensions = dimensions;
            this.Metric = metric;

            var points = this.InternalList.Select((p, i) => new CoordinateWithIndex(i, this.PointSelector(p))).ToArray();
            this.GenerateTree(ref this.rootNode, null, 0, points);
        }

        private Func<TNode, double[]> PointSelector { get; }

        /// <inheritdoc />
        public IEnumerable<int> Add(IEnumerable<TNode> 