using CanterburyUnderwater.PortalApi.Features.Bookings.Models;

namespace CanterburyUnderwater.PortalApi.Features.Bookings.Mine.Models;

public record CreateBookingModel
{
    public required DateOnly CheckInDate { get; init; }
    public required DateOnly CheckOutDate { get; init; }

    public string? GroupName { get; init; }
    public HashSet<int> Rooms { get; init; } = [];

    public Guid? ContractHolderId { get; set; }
    public List<BookingAttendeeModel> Attendees { get; set; } = [];
}