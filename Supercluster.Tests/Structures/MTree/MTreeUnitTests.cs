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
            var middleEntries = rootEntries[0].ChildNode.Entries.Concat(rootEntries[1].ChildNode.Entries).ToArray();
            var leafEntries = new List<MNodeEntry<int>>();

            foreach (var entry in middleEntries)
            {
                leafEntries.AddRange(entry.ChildNode.Entries);
            }

            // Check node entry values

            // Test Root entries
            Assert.That(mtree[rootEntries[0].Value], Is.EqualTo(points[0]));
            Assert.That(mtree[rootEntries[1].Value], Is.EqualTo(points[3]));

            // Test mid entries
            Assert.That(mtree[middleEntries[0].Value], Is.EqualTo(points[0]));
            Assert.That(mtree[middleEntries[1].Value], Is.EqualTo(points[2]));
            Assert.That(mtree[middleEntries[2].Value], Is.EqualTo(points[3]));
            Assert.That(mtree[middleEntries[3].Value], Is.EqualTo(points[4]));

            // Test leaf entries
            Assert.That(mtree[leafEntries[0].Value], Is.EqualTo(points[0]));
            Assert.That(mtree[leafEntries[1].Value], Is.EqualTo(points[1]));
            Assert.That(mtree[leafEntries[2].Value], Is.EqualTo(points[2]));
            Assert.That(mtree[leafEntries[3].Value], Is.EqualTo(points[5]));
            Assert.That(mtree[leafEntries[4].Value], Is.EqualTo(points[3]));
            Assert.That(mtree[leafEntries[5].Value], Is.EqualTo(points[7]));
            Assert.That(mtree[leafEntries[6].Value], Is.EqualTo(points[4]));
            Assert.That(mtree[leafEntries[7].Value], Is.EqualTo(points[6]));

            //    2. Ensure all node properties are correct at each level

            // Ensure that root node has no parent entry
            Assert.That(mtree.Root.ParentEntry, Is.Null);

            // Check node entry properties
            foreach (var entry in rootEntries)
            {
                Assert.That(entry.EnclosingNode, Is.EqualTo(mtree.Root));
                Assert.That(entry.ChildNode, Is.Not.Null);
                Assert.That(entry.DistanceFromParent, Is.EqualTo(-1));
                Assert.That(entry.CoveringRadius, Is.GreaterThan(0));
                Assert.That(entry.EnclosingNode.IsInternalNode, Is.True);
            }

            foreach (var entry in middleEntries)
            {
                Assert.That(entry.EnclosingNode.ParentEntry.EnclosingNode, Is.EqualTo(mtree.Root));
                Assert.That(entry.ChildNode, Is.Not.Null);
                Assert.That(entry.DistanceFromParent, Is.GreaterThanOrEqualTo(0));
                Assert.That(entry.CoveringRadius, Is.GreaterThan(0));
                Assert.That(entry.EnclosingNode.IsInternalNode, Is.True);
            }

            foreach (var entry in leafEntries)
            {
                Assert.That(entry.EnclosingNode.ParentEntry.EnclosingNode.ParentEntry.EnclosingNode, Is.EqualTo(mtree.Root));
                Assert.That(entry.ChildNode, Is.Null);
                Assert.That(entry.DistanceFromPar