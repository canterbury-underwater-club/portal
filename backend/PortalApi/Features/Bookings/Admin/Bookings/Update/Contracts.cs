using CanterburyUnderwater.PortalApi.Features.Bookings.Models;
using CanterburyUnderwater.PortalApi.WebAppExtensions;

namespace CanterburyUnderwater.PortalApi.Features.Bookings.Admin.Bookings.Update;

public record Contracts
{
    public record Request
    {
        public Optional<DateOnly> CheckInDate { get; init; }
        public Optional<DateOnly> CheckOutDate { get; init; }
        public Optional<Guid> PrimaryContactId { get; init; }
        public Optional<BookingStatusModel> BookingStatus { get; init; }
        public Optional<BookingBondStatusModel> BondStatus { get; init; }
        public Optional<string> GroupName { get; init; }
        public Optional<HashSet<int>> Rooms { get; init; }
        public Optional<Guid> ContractHolderId { get; set; }
        public Optional<List<BookingAttendeeModel>> Attendees { get; set; }
    }

    public record HandlerRequest : Request
    {
        public required Guid Id { get; init; }
    }
}