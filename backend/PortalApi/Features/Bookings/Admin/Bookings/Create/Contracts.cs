using CanterburyUnderwater.PortalApi.Features.Bookings.Admin.Bookings.Models;

namespace CanterburyUnderwater.PortalApi.Features.Bookings.Admin.Bookings.Create;

public class Contracts
{
    public record Request : CreateBookingModel;

    public record Response
    {
        public required BookingModel Booking { get; init; }
    }
}