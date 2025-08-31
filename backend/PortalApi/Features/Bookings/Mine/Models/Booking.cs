using CanterburyUnderwater.PortalApi.Features.Bookings.Models;

namespace CanterburyUnderwater.PortalApi.Features.Bookings.Mine.Models;

public record BookingModel : CreateBookingModel
{
    public required Guid Id { get; init; }
    public required DateTime CreatedAt { get; init; }
    public required BookingStatusModel BookingStatus { get; init; }
    public new List<BookingAttendeeModel> Attendees { get; set; } = [];
    public required BookingRatePlanModel RatePlan { get; init; }
}