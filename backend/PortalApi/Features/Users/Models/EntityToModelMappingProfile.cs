using AutoMapper;
using CanterburyUnderwater.PortalApi.DataAccess.Entities;

namespace CanterburyUnderwater.PortalApi.Features.Users.Models;

public class EntityToModelMappingProfile : Profile
{
    public EntityToModelMappingProfile()
    {
        CreateMap<User, UserModel>();
        CreateMap<MembershipStatus, MemberStatusModel>();
    }
}