﻿namespace Supercluster.Tests.Structures.MTree
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using global::Supercluster.Core;
    using global::Supercluster.MTree.NewDesign;
    using global::Supercluster.Structures.MTree;

    using NUnit.Framework;


    [TestFixture]
    public class MTreeUnitTests
    {
        /// <summary>
        /// Test a specific build of a two dimensional M-Tree with real valued coordinates.
        /// Test distance, properties, and placements of nodes.
        /// </summary>
        [Test]
        public void MtreeBuildTest()
        {

            var points = new double[][]
                  {
                      new double[] { 1, 3 },
                      new double[] { 2, 1 },
                      new double[] { 4, 2 },
          