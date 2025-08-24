using FluentValidation;

namespace CanterburyUnderwater.PortalApi.Features.Bookings.Models;

public class BookingAttendeeModelValidator : AbstractValidator<BookingAttendeeModel>
{
    public BookingAttendeeModelValidator()
    {
        RuleFor(a => a.FirstName)
            .NotEmpty()
            .MaximumLength(Constants.Validation.MaxNameLength);

        RuleFor(a => a.LastName)
            .NotEmpty()
            .MaximumLength(Constants.Validation.MaxNameLength);

        RuleFor(a => a.MembershipNumber)
            .GreaterThan(0)
            .When(a => a.MembershipNumber.HasValue);
    }
}