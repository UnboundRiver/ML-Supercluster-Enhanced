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
                new TestPoco { Name = "Test", Value = 33 },
                new TestPoco { Name = "Test", Value = 99 },
                new TestPoco { Name = "Test", Value = 0 },
                new TestPoco { Name = "Test", Value = 1 },
                new TestPoco { Name = "Test", Value = -13 },
                new TestPoco { Name = "Test", Value = 56 },
                new TestPoco { Name = "Test", Value = 3 },
                new TestPoco { Name = "Test", Value = -1 }
            };

        [Test]
        public void MaxIndexTest()
        {
            var maxIndex = this.numbers.MaxIndex();
            Assert.That(maxIndex, Is.EqualTo(1));
        }

        [Test]
        public void MinIndexTest()
        {
            var minIndex = this.numbers.MinIndex();
            Assert.That(minIndex, Is.EqualTo(4));
        }

        [Test]
        public void MinIndexTest_WithSelector()
        {
            var minIndex = this.numberWithStrings.MinIndex(p => p.Value);
            Assert.That(minIndex, Is.EqualTo(4));
        }

        [Test]
        public void MaxIndexTest_WithSelector()
        {
            var maxIndex = this.numberWithStrings.MaxIndex(p => p.Value);
            Assert.That(maxIndex, Is.EqualTo(1));
        }


        [Test]
        public void FirstIndexTest()
        {
            var firstIndex = this.numbers.FirstIndex(n => n < 0);
            Assert.That(firstIndex, Is.EqualTo(4));
        }

        [Test]
        public void FirstIndexOrDefaultTest()
        {
            var firstIndex = this.numbers.FirstIndexOrDefault(n => n < 0);
            Assert.That(firstIndex, Is.EqualTo(4));

            var defaultIndex = this.numbers.FirstIndexOrDefault(n => n > 100);
            Assert.That(defaultIndex, Is.EqualTo(-1));
        }

        [Test]
        public void LastIndexTest()
        {
            var lastIndex = this.numbers.LastIndex(n => n < 0);
            Assert.That(lastIndex, Is.EqualTo(7));
        }

        [Test]
        public void LastIndexOrDefaultTest(