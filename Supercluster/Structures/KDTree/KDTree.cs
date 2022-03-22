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
            this.C