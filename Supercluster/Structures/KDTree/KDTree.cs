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
        