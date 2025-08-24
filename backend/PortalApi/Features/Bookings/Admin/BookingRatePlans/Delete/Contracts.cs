namespace CanterburyUnderwater.PortalApi.Features.Bookings.Admin.BookingRatePlans.Delete;

public class Contracts
{
    public record Request
    {
        public Guid RatePlanId { get; init; }
    }
}