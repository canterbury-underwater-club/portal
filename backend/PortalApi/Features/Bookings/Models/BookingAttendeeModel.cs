namespace CanterburyUnderwater.PortalApi.Features.Bookings.Models;

public class BookingAttendeeModel
{
    public required string FirstName { get; set; }
    public required string LastName { get; set; }

    public BookingAttendeeTypeModel? AttendeeType { get; set; }
    public BookingAgeBracketModel? AgeBracket { get; set; }
    public int? MembershipNumber { get; set; }
}