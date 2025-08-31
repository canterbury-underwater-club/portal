using AutoMapper;
using CanterburyUnderwater.PortalApi.DataAccess.Entities;

namespace CanterburyUnderwater.PortalApi.Features.Bookings.Mine.Models;

public class EntityToModelMappingProfile : Profile
{
    public EntityToModelMappingProfile()
    {
        CreateMap<Booking, BookingModel>()
            .ForMember(d => d.RatePlan, o => o.MapFrom(s => s.BookingRatePlan));
    }
}