namespace CanterburyUnderwater.PortalApi.Features.Bookings.Admin.BookingContractHolders.Models;

public class BookingContractHolderModel
{
    public required Guid Id { get; init; }
    public required string Name { get; set; }
    public required bool IsActive { get; set; }
}