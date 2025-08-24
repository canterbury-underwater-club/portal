using System.ComponentModel.DataAnnotations;

namespace CanterburyUnderwater.PortalApi.DataAccess.Entities;

public enum BookingStatus
{
    Pending,
    Approved,
    Cancelled
}

public enum BookingBondStatus
{
    NotRequired,
    Required,
    Received,
    Returned
}

public class Booking : EntityDefaults
{
    public required DateOnly CheckInDate { get; set; }
    public required DateOnly CheckOutDate { get; set; }
    public required BookingStatus BookingStatus { get; set; } = BookingStatus.Pending;
    public required BookingBondStatus BondStatus { get; set; } = BookingBondStatus.NotRequired;

    [MaxLength(Constants.Validation.MaxBookingGroupNameLength)]
    public string? GroupName { get; set; }

    public required HashSet<int> Rooms { get; set; } = [];

    public required Guid PrimaryContactId { get; set; }
    public virtual User? PrimaryContact { get; set; }

    public Guid? ContractHolderId { get; set; }
    public virtual BookingContractHolder? ContractHolder { get; set; }

    public virtual ICollection<BookingAttendee> Attendees { get; set; } = [];

    public required Guid BookingRatePlanId { get; set; }
    public virtual BookingRatePlan? BookingRatePlan { get; set; }
}