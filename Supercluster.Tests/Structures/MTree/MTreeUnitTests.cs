namespace Supercluster.Tests.Structures.MTree
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
                      new double[] { 10, 10 },
                      new double[] { 9, 9 },
                      new double[] { 8, 9 },
                      new double[] { 9.5, 9.5 },
                      new double[] { 9.75, 9.75 }
                  };

            var mtree = new MTree<double[]>(Metrics.L2Norm, 3, points);
            /*
                1. Ensure all node values are in the right place
            */

            /*

            The M-Tree node entry values should look like the following:

                                        +-------------+
                                        |(1,3),(10,10)|
                                        +--+------+---+
                                           |      |
                           +---------------+      +---------------+
                           |                                      |
                     +-----+-----+                        +-------+-----+
                     |(1,3),(4,2)|                        |(10,10),(9,9)|
                     +--+-----+--+                        +---+------+--+
                        |     |                               |      |
                 +------+     +---+                  +--------+      +-----+
                 |                |                  |                     |
            +----+------+    +----+------+   +-------+-----------+   +-----+---------+
            |(1,3),(2,1)|    |(4,2),(8,9)|   |(10,10),(9.75,9.75)|   |(9,9),(9.5,9.5)|
            +-----------+    ------------+   +-------------------+   +---------------+

            */

            var rootEntries = mtree.Root.Entries;
            var middleEntries = rootEntries[0].ChildNode.Entries.Concat(rootEntr