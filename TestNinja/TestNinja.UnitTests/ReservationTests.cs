//using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;
using System;
using TestNinja.Fundamentals;

namespace TestNinja.UnitTests
{
    //[TestClass]
    //public class ReservationTests
    //{
    //    [TestMethod]
    //    //MethodName_Scenario_ExpectedBehavior - this is a common naming pattern
    //    public void CanBeCancelledBy_AdminCancelling_ReturnsTrue()
    //    {
    //        // Arrange (initialization of an object)
    //        var reservation = new Reservation();

    //        // Act (we act on this object)
    //        var result = reservation.CanBeCancelledBy(new User { IsAdmin = true });

    //        // Assert
    //        Assert.IsTrue(result);
    //    }

    //    [TestMethod]
    //    //MethodName_Scenario_ExpectedBehavior - this is a common naming pattern
    //    public void CanBeCancelledBy_SameUserCancelling_ReturnsTrue()
    //    {
    //        // Arrange (initialization of an object)
    //        var reservation = new Reservation();
    //        var user = new User { IsAdmin = false };
    //        reservation.MadeBy = user;

    //        // Act (we act on this object)
    //        var result = reservation.CanBeCancelledBy(user);

    //        // Assert
    //        Assert.IsTrue(result);
    //    }

    //    [TestMethod]
    //    //MethodName_Scenario_ExpectedBehavior - this is a common naming pattern
    //    public void CanBeCancelledBy_AnotherUserCancelling_ReturnsFalse()
    //    {
    //        // Arrange (initialization of an object)
    //        var reservation = new Reservation();

    //        // Act (we act on this object)
    //        var result = reservation.CanBeCancelledBy(new User { IsAdmin = false });

    //        // Assert
    //        Assert.IsFalse(result);
    //    }
    //}

    [TestFixture]
    public class ReservationTests
    {
        [Test]
        //[MethodName]_[Scenario]_[ExpectedBehavior] - this is a common naming pattern
        public void CanBeCancelledBy_AdminCancelling_ReturnsTrue()
        {
            // Arrange (initialization of an object)
            var reservation = new Reservation();

            // Act (we act on this object)
            var result = reservation.CanBeCancelledBy(new User { IsAdmin = true });

            // Assert
            Assert.That(result == true);
        }

        [Test]
        //MethodName_Scenario_ExpectedBehavior - this is a common naming pattern
        public void CanBeCancelledBy_SameUserCancelling_ReturnsTrue()
        {
            // Arrange (initialization of an object)
            var reservation = new Reservation();
            var user = new User { IsAdmin = false };
            reservation.MadeBy = user;

            // Act (we act on this object)
            var result = reservation.CanBeCancelledBy(user);

            // Assert
            Assert.That(result == true);
        }

        [Test]
        //MethodName_Scenario_ExpectedBehavior - this is a common naming pattern
        public void CanBeCancelledBy_AnotherUserCancelling_ReturnsFalse()
        {
            // Arrange (initialization of an object)
            var reservation = new Reservation();

            // Act (we act on this object)
            var result = reservation.CanBeCancelledBy(new User { IsAdmin = false });

            // Assert
            Assert.That(result == false);
        }
    }
}