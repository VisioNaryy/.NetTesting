using System;
using System.Collections.Generic;
using System.Linq;
using TestNinja.Mocking.Helpers;

namespace TestNinja.Mocking
{
    public static class BookingHelper
    {
        public static IBookingRepository BookingRepository { get; set; }
        public static void InitIBookingRepository(IBookingRepository bookingRepository)
        {
            BookingRepository = bookingRepository;
        }

        public static string OverlappingBookingsExist(Booking booking)
        {
            if (booking.Status == "Cancelled")
                return string.Empty;

            var overlappingBooking = BookingRepository.GetOverlappingBooking(booking);
            
            return overlappingBooking == null ? string.Empty : overlappingBooking.Reference;
        }
    }

    public interface IUnitOfWork
    {
        IQueryable<T> Query<T>();
    }

    public class UnitOfWork : IUnitOfWork
    {
        public IQueryable<T> Query<T>()
        {
            return new List<T>().AsQueryable();
        }
    }

    public class Booking
    {
        public string Status { get; set; }
        public int Id { get; set; }
        public DateTime ArrivalDate { get; set; }
        public DateTime DepartureDate { get; set; }
        public string Reference { get; set; }
    }
}