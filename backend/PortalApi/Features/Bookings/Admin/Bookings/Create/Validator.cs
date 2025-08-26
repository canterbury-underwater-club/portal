using System.Text.Json;
using CanterburyUnderwater.PortalApi.Features.Bookings.Admin.Bookings.Models;
using CanterburyUnderwater.PortalApi.Features.Bookings.Models;
using FluentValidation;

namespace CanterburyUnderwater.PortalApi.Features.Bookings.Admin.Bookings.Create;

public class Validator : AbstractValidator<Contracts.Request>
{
    public Validator()
    {
        RuleFor(m => m.CheckInDate)
            .NotEmpty();

        RuleFor(m => m.CheckOutDate)
            .NotEmpty()
            .Must((m, to) => to > m.CheckInDate)
            .WithMessage(
                $"{nameof(CreateBookingModel.CheckOutDate)} must be later than {nameof(CreateBookingModel.CheckInDate)}.");

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
            .SetValidator(new BookingAttendeeModelValidator());

        When(m => m.ContractHolderId != null, () =>
        {
            RuleForEach(m => m.Attendees).ChildRules(a =>
            {
                a.RuleFor(x => x.AttendeeType)
                    .Null().WithMessage(
                        $"{nameof(BookingAttendeeModel.AttendeeType)} must be null for contract bookings.");
                a.RuleFor(x => x.AgeBracket)
                    .Null().WithMessage(
                        $"{nameof(BookingAttendeeModel.AgeBracket)} must be null for contract bookings.");
            });
        });

        When(m => m.ContractHolderId == null, () =>
        {
            RuleForEach(m => m.Attendees).ChildRules(a =>
            {
                a.RuleFor(x => x.AttendeeType)
                    .NotNull().WithMessage(
                        $"{nameof(BookingAttendeeModel.AttendeeType)} is required for standard bookings.")
                    .IsInEnum();
                a.RuleFor(x => x.AgeBracket)
                    .NotNull().WithMessage(
                        $"{nameof(BookingAttendeeModel.AgeBracket)} is required for standard bookings.")
                    .IsInEnum();
            });
        });
    }
}