using System.ComponentModel.DataAnnotations;

namespace CanterburyUnderwater.PortalApi.DataAccess.Entities;

public enum BookingRateType
{
    Standard = 0,
    Contract = 1
}

public enum BookingFeeBasis
{
    PerAttendeePerBooking = 0,
    PerBookingFlat = 1
}

public class BookingRatePlan : EntityDefaults
{
    [MinLength(Constants.Validation.MinBookingRatePlanNameLength)]
    [MaxLength(Constants.Validation.MaxBookingRatePlanNameLength)]
    public required string Name { get; set; }

    public required DateOnly EffectiveFrom { get; set; }

    public virtual ICollection<BookingRate> Rates { get; set; } = [];
    public virtual ICollection<BookingFee> Fees { get; set; } = [];
}

public class BookingRate : EntityDefaults
{
    public required Guid BookingRatePlanId { get; set; }
    public virtual BookingRatePlan? BookingRatePlan { get; set; }

    public required BookingRateType RateType { get; set; }

    public Guid? ContractHolderId { get; set; }
    public virtual BookingContractHolder? ContractHolder { get; set; }

    public BookingAttendeeType? AttendeeType { get; set; }
    public BookingAgeBracket? AgeBracket { get; set; }

    public required long UnitPriceCents { get; set; }
}

public class BookingFee : EntityDefaults
{
    public required Guid BookingRatePlanId { get; set; }
    public virtual BookingRatePlan? BookingRatePlan { get; set; }

    [MinLength(Constants.Validation.MinBookingFeeNameLength)]
    [MaxLength(Constants.Validation.MaxBookingFeeNameLength)]
    public required string Name { get; set; }

    public required BookingFeeBasis Basis { get; set; }

    public required long UnitPriceCents { get; set; }
}