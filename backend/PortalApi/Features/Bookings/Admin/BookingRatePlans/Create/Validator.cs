using CanterburyUnderwater.PortalApi.Features.Bookings.Models;
using FluentValidation;

namespace CanterburyUnderwater.PortalApi.Features.Bookings.Admin.BookingRatePlans.Create;

public class Validator : AbstractValidator<Contracts.Request>
{
    public Validator()
    {
        RuleFor(m => m.Name)
            .NotEmpty()
            .MinimumLength(Constants.Validation.MinBookingRatePlanNameLength)
            .MaximumLength(Constants.Validation.MaxBookingRatePlanNameLength);

        RuleFor(m => m.EffectiveFrom)
            .NotEmpty()
            .Must(date => date >= DateOnly.FromDateTime(DateTime.UtcNow))
            .WithMessage($"{nameof(BookingRatePlanModel.EffectiveFrom)} must be today or a future date.");

        RuleFor(m => m.Rates)
            .NotNull()
            .Must(r => r.Count > 0).WithMessage("At least one rate is required.");

        RuleForEach(m => m.Rates)
            .SetValidator(new BookingRateModelValidator());

        RuleFor(m => m.Rates)
            .Must(NoDuplicateStandardRates)
            .WithMessage(
                $"Duplicate standard rate detected for the same {nameof(BookingRateModel.AttendeeType)} + {nameof(BookingRateModel.AgeBracket)}.");

        RuleFor(m => m.Rates)
            .Must(NoDuplicateContractRates)
            .WithMessage($"Duplicate contract rate detected for the same {nameof(BookingRateModel.ContractHolderId)}.");

        RuleFor(m => m.Fees)
            .NotNull();

        RuleForEach(m => m.Fees)
            .SetValidator(new BookingFeeModelValidator());

        RuleFor(m => m.Fees)
            .Must(NoDuplicateFeeNames)
            .WithMessage("Duplicate fee names are not allowed (case-insensitive).");
    }

    private static bool NoDuplicateStandardRates(IReadOnlyCollection<BookingRateModel> rates)
    {
        var duplicates = rates
            .Where(r => r.RateType == BookingRateTypeModel.Standard)
            .GroupBy(r => new { r.AttendeeType, r.AgeBracket })
            .Any(g => g.Key.AttendeeType == null || g.Key.AgeBracket == null || g.Count() > 1);

        return !duplicates;
    }

    private static bool NoDuplicateContractRates(IReadOnlyCollection<BookingRateModel> rates)
    {
        var duplicates = rates
            .Where(r => r.RateType == BookingRateTypeModel.Contract)
            .GroupBy(r => r.ContractHolderId)
            .Any(g => g.Key == null || g.Count() > 1);

        return !duplicates;
    }

    private static bool NoDuplicateFeeNames(IReadOnlyCollection<BookingFeeModel> fees)
    {
        var set = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
        return fees.Select(f => f.Name.Trim()).All(name => set.Add(name));
    }
}

public sealed class BookingRateModelValidator : AbstractValidator<BookingRateModel>
{
    public BookingRateModelValidator()
    {
        RuleFor(r => r.RateType).IsInEnum();
        RuleFor(r => r.UnitPriceCents).GreaterThanOrEqualTo(0);

        When(r => r.RateType == BookingRateTypeModel.Standard, () =>
        {
            RuleFor(r => r.AttendeeType)
                .NotNull().WithMessage($"{nameof(BookingRateModel.AttendeeType)} is required for standard rates.")
                .IsInEnum();

            RuleFor(r => r.AgeBracket)
                .NotNull().WithMessage($"{nameof(BookingRateModel.AgeBracket)} is required for standard rates.")
                .IsInEnum();

            RuleFor(r => r.ContractHolderId)
                .Null().WithMessage($"{nameof(BookingRateModel.ContractHolderId)} must be null for standard rates.");
        });

        When(r => r.RateType == BookingRateTypeModel.Contract, () =>
        {
            RuleFor(r => r.ContractHolderId)
                .NotNull().WithMessage($"{nameof(BookingRateModel.ContractHolderId)} is required for contract rates.");

            RuleFor(r => r.AttendeeType)
                .Null().WithMessage($"{nameof(BookingRateModel.AttendeeType)} must be null for contract rates.");

            RuleFor(r => r.AgeBracket)
                .Null().WithMessage($"{nameof(BookingRateModel.AgeBracket)} must be null for contract rates.");
        });
    }
}

public sealed class BookingFeeModelValidator : AbstractValidator<BookingFeeModel>
{
    public BookingFeeModelValidator()
    {
        RuleFor(f => f.Name)
            .NotEmpty()
            .MinimumLength(Constants.Validation.MinBookingFeeNameLength)
            .MaximumLength(Constants.Validation.MaxBookingFeeNameLength);

        RuleFor(f => f.Basis)
            .IsInEnum();

        RuleFor(f => f.UnitPriceCents)
            .GreaterThanOrEqualTo(0);
    }
}