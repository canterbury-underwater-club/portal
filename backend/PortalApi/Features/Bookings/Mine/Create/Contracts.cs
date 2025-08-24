using CanterburyUnderwater.PortalApi.Features.Bookings.Mine.Models;

namespace CanterburyUnderwater.PortalApi.Features.Bookings.Mine.Create;

public class Contracts
{
    public record Request : CreateBookingModel
    {
    }

    public record Response : BookingModel
    {
    }
}