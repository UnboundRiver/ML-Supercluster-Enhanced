namespace Supercluster.Tests.DataStructures.KDTree
{
    using NUnit.Framework;

    using Supercluster.Structures;

    [TestFixture]
    public class BoundedPriorityTest
    {
        [Test]
        public void InsertTest()
        {
            var bp = new BoundablePriorityList<int, double>(3, true)
                         {
                             { 34, 98744.90383 },
                             { 23, 67.39030 },
                             { 2, 2 },
                             { 89, 3