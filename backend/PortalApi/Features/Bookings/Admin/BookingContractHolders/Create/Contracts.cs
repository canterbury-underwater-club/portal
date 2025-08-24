using CanterburyUnderwater.PortalApi.Features.Bookings.Admin.BookingContractHolders.Models;

namespace CanterburyUnderwater.PortalApi.Features.Bookings.Admin.BookingContractHolders.Create;

public class Contracts
{
    public record Request
    {
        public required string Name { get; init; }
    }

    public record Response
    {
        public required BookingContractHolderModel ContractHolder { get; init; }
    }
}