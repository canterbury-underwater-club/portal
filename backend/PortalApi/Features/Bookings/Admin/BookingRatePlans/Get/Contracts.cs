using CanterburyUnderwater.PortalApi.Features.Bookings.Models;

namespace CanterburyUnderwater.PortalApi.Features.Bookings.Admin.BookingRatePlans.Get;

public class Contracts
{
    public record Request
    {
        public Guid RatePlanId { get; init; }
    }

    public record Response
    {
        public required BookingRatePlanModel RatePlan { get; init; }
    }
}