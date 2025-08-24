using AutoMapper;
using CanterburyUnderwater.PortalApi.DataAccess.Entities;

namespace CanterburyUnderwater.PortalApi.Features.Bookings.Admin.BookingRatePlans.Create;

public class ModelToEntityMappingProfile : Profile
{
    public ModelToEntityMappingProfile()
    {
        CreateMap<Contracts.Request, BookingRatePlan>();
    }
}