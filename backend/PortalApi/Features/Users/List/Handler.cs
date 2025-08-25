using AutoMapper;
using CanterburyUnderwater.PortalApi.DataAccess;
using CanterburyUnderwater.PortalApi.EndpointHandling;
using CanterburyUnderwater.PortalApi.Features.Users.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace CanterburyUnderwater.PortalApi.Features.Users.List;

public class Handler(PortalDbContext db, IMapper mapper)
    : IResponseEndpointHandler<Ok<Contracts.Response>>
{
    public async Task<Ok<Contracts.Response>> HandleAsync(CancellationToken ct = default)
    {
        var users = await db.Users.ToListAsync(ct);

        return TypedResults.Ok(new Contracts.Response
        {
            Users = mapper.Map<List<UserModel>>(users)
        });
    }
}