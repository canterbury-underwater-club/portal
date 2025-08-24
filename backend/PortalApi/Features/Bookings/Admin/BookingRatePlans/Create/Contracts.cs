using CanterburyUnderwater.PortalApi.Features.Bookings.Models;

namespace CanterburyUnderwater.PortalApi.Features.Bookings.Admin.BookingRatePlans.Create;

public class Contracts
{
    public record Request
    {
        public required string Name { get; init; }
        public required DateOnly EffectiveFrom { get; init; }
        public required List<BookingRateModel> Rates { get; init; } = [];
        public required List<BookingFeeModel> Fees { get; init; } = [];
    }

    public record Response
    {
        public required BookingRatePlanModel RatePlan { get; init; }
    }
}