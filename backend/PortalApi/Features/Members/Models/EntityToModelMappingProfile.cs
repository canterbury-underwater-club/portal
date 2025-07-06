using AutoMapper;

namespace CanterburyUnderwater.PortalApi.Features.Members.Models;

public class EntityToModelMappingProfile : Profile
{
    public EntityToModelMappingProfile()
    {
        CreateMap<DataAccess.Entities.Member, Member>();
    }
}