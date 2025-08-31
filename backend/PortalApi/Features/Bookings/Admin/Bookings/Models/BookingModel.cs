using CanterburyUnderwater.PortalApi.Features.Bookings.Admin.BookingContractHolders.Models;
using CanterburyUnderwater.PortalApi.Features.Bookings.Models;

namespace CanterburyUnderwater.PortalApi.Features.Bookings.Admin.Bookings.Models;

public record BookingModel : CreateBookingModel
{
    public required Guid Id { get; init; }
    public required DateTime CreatedAt { get; init; }
    public new List<BookingAttendeeModel> Attendees { get; set; } = [];
    public required BookingRatePlanModel RatePlan { get; init; }
    public BookingContractHolderModel? ContractHolder { get; init; }
}