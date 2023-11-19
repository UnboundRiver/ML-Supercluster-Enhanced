using System;
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
            va