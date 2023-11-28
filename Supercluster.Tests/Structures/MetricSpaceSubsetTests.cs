﻿using System;
using System.Linq;

namespace Supercluster.Tests.Structures
{
    using System.Diagnostics;

    using NUnit.Framework;

    using Supercluster.Core;
    using Supercluster.Structures;

    using Utilities = Supercluster.Tests.Utilities;

    [TestFixture]
    public class MetricSpaceSubsetTests
    {
        [Test]
        public void RadialSearchTest()
        {
            var dataSize = 1000;
            var testDataSize = 1;
            var range = 1000;
            var radius = 100;

            var realData = Utilities.GenerateDoubles(dataSize, range, 2);
            var testData = Utilities.GenerateDoubles(testDataSize, range, 2);
            var metricSpaceSubset = new MetricSpaceSubset<double[]>(realData, Metrics.L2Norm);

            // structure search
            var resultsList = metricSpaceSubset.RadialSearch(testData[0], radius);

            // linear search
            var linearResults = realData.Where(point => Metrics.L2Norm(point, testData[0]) <= radius);

            // sort results
            var sortedResults = resultsList.OrderBy(r => r[0]).ThenBy(r => r[1]).ToArray();
            var sortedLinearResults = linearResults.OrderBy(r => r[0]).ThenBy(r => r[1]).ToArray();

            // test results
            Assert.That(sortedResults.Length == sortedLinearResults.Length);
            for (int i = 0