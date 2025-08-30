using CanterburyUnderwater.PortalApi.Features.Bookings.Admin.Bookings.Models;

namespace CanterburyUnderwater.PortalApi.Features.Bookings.Mine.List;

public record Contracts
{
    public record Request
    {
        public DateOnly? From { get; init; }
        public DateOnly? To { get; init; }
    }

    public record Response
    {
        public required List<BookingModel> Bookings { get; set; }
    }
}