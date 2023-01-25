using System.Linq;

namespace TestNinja.Mocking.Helpers
{
    public interface IBookingRepository
    {
        Booking GetOverlappingBooking(Booking booking);
    }

    public class BookingRepository : IBookingRepository
    {
        public Booking GetOverlappingBooking(Booking booking)
        {
            var unitOfWork = new UnitOfWork();
            var bookings =
                unitOfWork.Query<Booking>()
                    .Where(
                        b => b.Id != booking.Id && b.Status != "Cancelled");

            var overlappingBooking =
                bookings.FirstOrDefault(
                    b =>
                        booking.ArrivalDate >= b.ArrivalDate
                        && booking.ArrivalDate < b.DepartureDate
                        || booking.DepartureDate > b.ArrivalDate
                        && booking.DepartureDate <= b.DepartureDate);

            return overlappingBooking;
        }
    }
}
