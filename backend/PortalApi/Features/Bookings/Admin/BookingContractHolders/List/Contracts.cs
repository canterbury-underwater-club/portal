using CanterburyUnderwater.PortalApi.Features.Bookings.Admin.BookingContractHolders.Models;

namespace CanterburyUnderwater.PortalApi.Features.Bookings.Admin.BookingContractHolders.List;

public class Contracts
{
    public record Response
    {
        public required List<BookingContractHolderModel> ContractHolders { get; init; }
    }
}