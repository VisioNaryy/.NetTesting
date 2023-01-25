using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestNinja.Fundamentals;

namespace TestNinja.UnitTests
{
    [TestFixture]
    public class MathTests
    {
        // SetUp
        // TearDown
        private Math _math;

        [SetUp]
        public void SetUp()
        {
            _math = new Math();
        }


        [Test]
        [Ignore("Did i asked?")]
        public void Add_WhenCalled_ReturnsTheSumOfArguments()
        {
            var result = _math.Add(1, 3);

            Assert.That(result, Is.EqualTo(4));
        }

        [Test]
        [TestCase(2, 1, 2)]
        [TestCase(1, 2, 2)]
        [TestCase(1, 1, 1)]
        public void Max_WhenCalled_ReturnsTheGreaterArgument(int a, int b, int expectedResult)
        {
            var result = _math.Max(a, b);

            Assert.That(result, Is.EqualTo(expectedResult));
        }

        [Test]
        public void GetOddNumbers_LimitIsGreaterThanZero_ReturnOddNumbersUpToLimit()
        {
            var result = _math.GetOddNumbers(5);

            //Assert.That(result, Is.Not.Empty);

            //Assert.That(result.Count(), Is.EqualTo(3));

            //Assert.That(result, Does.Contain(1));
            //Assert.That(result, Does.Contain(3));
            //Assert.That(result, Does.Contain(5));

            Assert.That(result, Is.EquivalentTo(new[] { 1, 3, 5 }));

            //Assert.That(result, Is.Ordered);
            //Assert.That(result, Is.Unique);
        }


        //[Test]
        //public void Max_FirstArgumentIsGreater_ReturnsTheFirstArgument()
        //{
        //    var result = _math.Max(3, 1);

        //    Assert.That(result, Is.EqualTo(3));
        //}

        //[Test]
        //public void Max_SecondArgumentIsGreater_ReturnsTheSecondArgument()
        //{
        //    var result = _math.Max(1, 3);

        //    Assert.That(result, Is.EqualTo(3));
        //}

        //[Test]
        //public void Max_SecondArgumentAreEqual_ReturnsTheSameArgument()
        //{
        //    var result = _math.Max(1, 1);

        //    Assert.That(result, Is.EqualTo(1));
        //}

    }
}
