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
                             