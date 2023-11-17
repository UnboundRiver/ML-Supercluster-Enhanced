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
            var dataSize