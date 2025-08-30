using CanterburyUnderwater.PortalApi.Features.Bookings.Mine.Models;

namespace CanterburyUnderwater.PortalApi.Features.Bookings.Mine.Get;

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