using CanterburyUnderwater.PortalApi.DataAccess.Entities;
using FluentValidation;

namespace CanterburyUnderwater.PortalApi.DataAccess.Validators;

public class BookingAttendeeValidator : AbstractValidator<BookingAttendee>
{
    public BookingAttendeeValidator()
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