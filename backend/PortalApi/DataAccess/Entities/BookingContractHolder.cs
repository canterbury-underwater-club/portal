using System.ComponentModel.DataAnnotations;

namespace CanterburyUnderwater.PortalApi.DataAccess.Entities;

public class BookingContractHolder : EntityDefaults
{
    [MinLength(Constants.Validation.MinContractNameLength)]
    [MaxLength(Constants.Validation.MaxContractNameLength)]
    public required string Name { get; set; }

    public required bool IsActive { get; set; }
}