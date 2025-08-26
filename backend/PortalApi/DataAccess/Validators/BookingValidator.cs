using System.Text.Json;
using CanterburyUnderwater.PortalApi.DataAccess.Entities;
using FluentValidation;

namespace CanterburyUnderwater.PortalApi.DataAccess.Validators;

public class BookingValidator : AbstractValidator<Booking>
{
    public BookingValidator()
    {
        RuleFor(m => m.CheckInDate)
            .NotEmpty();

        RuleFor(m => m.CheckOutDate)
            .NotEmpty()
            .Must((m, to) => to > m.CheckInDate)
            .WithMessage(
                $"{nameof(Booking.CheckOutDate)} must be later than {nameof(Booking.CheckInDate)}.");

        RuleFor(m => m.PrimaryContactId)
            .NotEmpty();

        RuleFor(m => m.BookingStatus).IsInEnum();
        RuleFor(m => m.BondStatus).IsInEnum();

        RuleFor(m => m.GroupName)
            .MaximumLength(Constants.Validation.MaxBookingGroupNameLength);

        RuleFor(m => m.Rooms)
            .NotNull()
            .Must(r => r.Count > 0)
            .WithMessage("At least one room must be selected.")
            .Must(r => r.All(id => Constants.Validation.RoomNumbers.Contains(id)))
            .WithMessage($"All rooms must be in {JsonSerializer.Serialize(Constants.Validation.RoomNumbers)}.");

        RuleFor(m => m.Attendees)
            .NotNull()
            .Must(a => a.Count > 0).WithMessage("At least one attendee is required.");

        RuleForEach(m => m.Attendees)
            .SetValidator(new BookingAttendeeValidator());

        When(m => m.ContractHolderId != null, () =>
        {
            RuleForEach(m => m.Attendees).ChildRules(a =>
            {
                a.RuleFor(x => x.AttendeeType)
                    .Null().WithMessage(
                        $"{nameof(BookingAttendee.AttendeeType)} must be null for contract bookings.");
                a.RuleFor(x => x.AgeBracket)
                    .Null().WithMessage(
                        $"{nameof(BookingAttendee.AgeBracket)} must be null for contract bookings.");
            });
        });

        When(m => m.ContractHolderId == null, () =>
        {
            RuleForEach(m => m.Attendees).ChildRules(a =>
            {
                a.RuleFor(x => x.AttendeeType)
                    .NotNull().WithMessage(
                        $"{nameof(BookingAttendee.AttendeeType)} is required for standard bookings.")
                    .IsInEnum();
                a.RuleFor(x => x.AgeBracket)
                    .NotNull().WithMessage(
                        $"{nameof(BookingAttendee.AgeBracket)} is required for standard bookings.")
                    .IsInEnum();
            });
        });
    }
}