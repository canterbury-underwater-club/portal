using AutoMapper;
using CanterburyUnderwater.Endpoints;
using CanterburyUnderwater.PortalApi.DataAccess;
using CanterburyUnderwater.PortalApi.Features.Users.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace CanterburyUnderwater.PortalApi.Features.Users.List;

public class Handler(PortalDbContext dbContext, IMapper mapper)
    : IResponseEndpointHandler<Ok<Contracts.Response>>
{
    public async Task<Ok<Contracts.Response>> HandleAsync(CancellationToken cancellationToken = default)
    {
        var users = await dbContext.Users.ToListAsync(cancellationToken);

        return TypedResults.Ok(new Contracts.Response
        {
            Users = mapper.Map<List<UserModel>>(users)
        });
    }
}