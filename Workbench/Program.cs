
﻿namespace Workbench
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;
    using System.Linq;
    using System.Runtime;
    using System.Runtime.CompilerServices;
    using System.Text;

    using Supercluster.Classification;
    using Supercluster.MTree;
    using Supercluster.MTree.Tests;
    using Supercluster.Structures.MTree;
    using Supercluster.Tests.Structures.MTree.Supercluster.MTree;

    class Program
    {
        static void Main(string[] args)
        {
            var mtreeStopwatch = new Stopwatch();
            var fastMtreeStopWatch = new Stopwatch();

            var originalTimes = new List<long>();
            var fastTimes = new List<long>();

            var dataSize = 100000;
            var testDataSize = 50;
            var range = 1000000;
            var neighbors = 10;
            var dimensions = 1000;

            Console.WriteLine($"{nameof(dataSize)}: {dataSize}");
            Console.WriteLine($"{nameof(dimensions)}: {dimensions}");
            Console.WriteLine($"{nameof(neighbors)}: {neighbors}");
            Console.WriteLine();

            var testData = Supercluster.Tests.Utilities.GenerateDoubles(testDataSize, range, dimensions);
            for (int index = 0; index < testData.Length; index++)
            {

                var treeData = Supercluster.Tests.Utilities.GenerateDoubles(dataSize, range, dimensions);
                var target = testData[index];


                // 1. Build Trees
                var fastMtree = new FastMTree<double[]> { Capacity = 3, Metric = Metrics.L2Norm_Double };
                foreach (var point in treeData)
                {
                    fastMtree.Add(point);
                }


                var mtree = new MTree<double[]> { Capacity = 3, Metric = Metrics.L2Norm_Double };
                foreach (var point in treeData)
                {
                    mtree.Add(point);
                }
