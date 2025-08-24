using System.ComponentModel.DataAnnotations;

namespace CanterburyUnderwater.PortalApi.DataAccess.Entities;

public class BookingAttendee : EntityDefaults
{
    public required Guid BookingId { get; set; }
    public virtual Booking? Booking { get; set; }

    [MaxLength(Constants.Validation.MaxNameLength)]
    public required string FirstName { get; set; }

    [MaxLength(Constants.Validation.MaxNameLength)]
    public required string LastName { get; set; }

    public BookingAttendeeType? AttendeeType { get; set; }
    public BookingAgeBracket? AgeBracket { get; set; }
    public int? MembershipNumber { get; set; }

    // Optional: if this attendee corresponds to a registered User (e.g., a member)
    public Guid? LinkedUserId { get; set; }
    public virtual User? LinkedUser { get; set; }
}