using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestNinja.Mocking;
using TestNinja.Mocking.Helpers;

namespace TestNinja.UnitTests.Mocking
{
    [TestFixture]
    public class BookingHelperTests
    {
        private Mock<IBookingRepository> _bookingRepository;

        private string _fullPath;

        [SetUp]
        public void SetUp()
        {
            _bookingRepository = new Mock<IBookingRepository>();
            //_fullPath = File.ReadAllText(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName + @"\TestFiles\" + "TextFile1.txt");
        }

        [Test]
        public void OverlappingBookingsExist_BookingStatusIsCancelled_ReturnEmptyString()
        {
            BookingHelper.InitIBookingRepository(_bookingRepository.Object);

            var booking = new Booking { Status = "Cancelled" };

            var result = BookingHelper.OverlappingBookingsExist(booking);

            Assert.That(result, Is.EqualTo(string.Empty));
        }

        [Test]
        public void OverlappingBookingsExist_ValidBookingStatus_ReturnEmptyString()
        {      
            BookingHelper.InitIBookingRepository(_bookingRepository.Object);

            var booking = new Booking { Status = "NotCancelled" };

            _bookingRepository.Setup(x => x.GetOverlappingBooking(booking)).Returns(value: null);

            var result = BookingHelper.OverlappingBookingsExist(booking);

            Assert.That(result, Is.EqualTo(string.Empty));
        }

        [Test]
        public void OverlappingBookingsExist_ValidBookingStatus_ReturnReferenceField()
        {
            BookingHelper.InitIBookingRepository(_bookingRepository.Object);

            var booking = new Booking { Status = "NotCancelled" };

            _bookingRepository.Setup(x => x.GetOverlappingBooking(booking)).Returns<Booking>(x => new Booking { Reference = "New Reference" });

            var result = BookingHelper.OverlappingBookingsExist(booking);

            Assert.That(result, Is.Not.EqualTo(string.Empty));
        }
    }
}
