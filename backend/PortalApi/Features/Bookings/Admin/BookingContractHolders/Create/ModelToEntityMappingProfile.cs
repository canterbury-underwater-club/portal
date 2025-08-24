using AutoMapper;
using CanterburyUnderwater.PortalApi.DataAccess.Entities;

namespace CanterburyUnderwater.PortalApi.Features.Bookings.Admin.BookingContractHolders.Create;

public class ModelToEntityMappingProfile : Profile
{
    public ModelToEntityMappingProfile()
    {
        CreateMap<Contracts.Request, BookingContractHolder>()
            .ForMember(d => d.IsActive, o => o.MapFrom(_ => true));
    }
}