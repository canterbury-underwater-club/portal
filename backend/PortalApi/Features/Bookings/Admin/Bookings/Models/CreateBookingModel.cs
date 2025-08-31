using CanterburyUnderwater.PortalApi.Features.Bookings.Models;

namespace CanterburyUnderwater.PortalApi.Features.Bookings.Admin.Bookings.Models;

public record CreateBookingModel
{
    public required DateOnly CheckInDate { get; init; }
    public required DateOnly CheckOutDate { get; init; }
    public required Guid PrimaryContactId { get; init; }
    public required BookingStatusModel BookingStatus { get; init; }
    public required BookingBondStatusModel BondStatus { get; init; }

    public string? GroupName { get; init; }
    public HashSet<int> Rooms { get; init; } = [];

    public Guid? ContractHolderId { get; set; }
    public List<CreateBookingAttendeeModel> Attendees { get; set; } = [];
}