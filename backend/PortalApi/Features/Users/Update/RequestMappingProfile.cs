using AutoMapper;

namespace CanterburyUnderwater.PortalApi.Features.Users.Update;

public class RequestMappingProfile : Profile
{
    public RequestMappingProfile()
    {
        CreateMap<Contracts.Request, Contracts.HandlerRequest>()
            .ForMember(dest => dest.Id, opt => opt.Ignore());
    }
}