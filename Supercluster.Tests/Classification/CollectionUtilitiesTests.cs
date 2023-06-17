namespace Supercluster_Tests
{
    using System.Linq;

    using NUnit.Framework;

    using Supercluster;

    /// <summary>
    /// Unit tests for the <see cref="CollectionUtilities"/> class.
    /// </summary>
    [TestFixture]
    public class CollectionUtilitiesTests
    {
        #region LinqToIndexes Tests

        public class TestPoco
        {
            public string Name { get; set; }
            public int Value { get; set; }
        }

        public int[] numbers = { 33, 99, 0, 1, -13, 56, 3, -1 };

        public TestPoco[] numberWithStrings =
            {
                new Tes