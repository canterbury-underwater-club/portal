using CanterburyUnderwater.PortalApi.Features.Bookings.Admin.Bookings.Models;

namespace CanterburyUnderwater.PortalApi.Features.Bookings.Admin.Bookings.Get;

public record Contracts
{
    public record Request
    {
        public Guid BookingId { get; init; }
    }

    public record Response
    {
        public required BookingModel Booking { get; set; }
    }
}