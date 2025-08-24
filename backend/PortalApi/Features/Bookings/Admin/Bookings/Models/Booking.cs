namespace CanterburyUnderwater.PortalApi.Features.Bookings.Admin.Bookings.Models;

public record BookingModel : CreateBookingModel
{
    public required Guid Id { get; init; }
    public required DateTime CreatedAt { get; init; }
}