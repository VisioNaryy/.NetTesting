using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestNinja.Fundamentals;

namespace TestNinja.UnitTests
{
    [TestFixture]
    public class FizzBuzzTests
    {
        //private FizzBuzz _fizzBuzz;

        //[SetUp]
        //public void SetUp()
        //{
        //    _fizzBuzz = new FizzBuzz();
        //}

        [Test]
        [TestCase(15)]
        [TestCase(30)]
        [TestCase(45)]
        public void GetOutput_NumberIsDivisibleBy3And5_ReturnFizzBuzz(int number)
        {
            var result = FizzBuzz.GetOutput(number);

            Assert.That(result, Is.EqualTo("FizzBuzz"));
        }

        [Test]
        [TestCase(3)]
        [TestCase(12)]
        [TestCase(21)]
        public void GetOutput_NumberIsIsDivisibleBy3_ReturnFizz(int number)
        {
            var result = FizzBuzz.GetOutput(number);

            Assert.That(result, Is.EqualTo("Fizz"));
        }

        [Test]
        [TestCase(5)]
        [TestCase(25)]
        [TestCase(40)]
        public void GetOutput_NumberIsDivisibleBy5_ReturnBuzz(int number)
        {
            var result = FizzBuzz.GetOutput(number);

            Assert.That(result, Is.EqualTo("Buzz"));
        }

        [Test]
        [TestCase(2)]
        [TestCase(13)]
        [TestCase(22)]
        public void GetOutput_NumberIsNotDivisibleBy3Or5_ReturnThisNumber(int number)
        {
            var result = FizzBuzz.GetOutput(number);

            Assert.That(result, Is.EqualTo(number.ToString()));
        }
    }
}
