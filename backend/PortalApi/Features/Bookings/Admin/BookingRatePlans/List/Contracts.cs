using CanterburyUnderwater.PortalApi.Features.Bookings.Models;

namespace CanterburyUnderwater.PortalApi.Features.Bookings.Admin.BookingRatePlans.List;

public class Contracts
{
    public record Response
    {
        public required List<BookingRatePlanModel> RatePlans { get; init; }
    }
}