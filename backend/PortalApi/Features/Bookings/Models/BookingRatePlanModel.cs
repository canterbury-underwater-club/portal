namespace CanterburyUnderwater.PortalApi.Features.Bookings.Models;

public record BookingRatePlanModel
{
    public Guid Id { get; init; }
    public required string Name { get; init; }
    public required DateOnly EffectiveFrom { get; init; }
    public required List<BookingRateModel> Rates { get; init; } = [];
    public required List<BookingFeeModel> Fees { get; init; } = [];

    public required bool IsCurrent { get; set; }
}

public enum BookingRateTypeModel
{
    Standard = 0,
    Contract = 1
}

public enum BookingFeeBasisModel
{
    PerAttendeePerBooking = 0,
    PerBookingFlat = 1
}

public record BookingRateModel
{
    public required BookingRateTypeModel RateType { get; init; }
    public Guid? ContractHolderId { get; init; }
    public BookingAttendeeTypeModel? AttendeeType { get; init; }
    public BookingAgeBracketModel? AgeBracket { get; init; }
    public required long UnitPriceCents { get; init; }
}

public record BookingFeeModel
{
    public required string Name { get; init; }
    public required BookingFeeBasisModel Basis { get; init; }
    public required long UnitPriceCents { get; init; }
}