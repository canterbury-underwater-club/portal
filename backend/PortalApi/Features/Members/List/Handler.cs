using AutoMapper;
using CanterburyUnderwater.Endpoints;
using CanterburyUnderwater.PortalApi.DataAccess;
using CanterburyUnderwater.PortalApi.Features.Members.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace CanterburyUnderwater.PortalApi.Features.Members.List;

public class Handler(PortalDbContext dbContext, IMapper mapper, IHttpContextAccessor httpContextAccessor)
    : IResponseEndpointHandler<Ok<Contracts.Response>>
{
    public async Task<Ok<Contracts.Response>> HandleAsync(CancellationToken cancellationToken = default)
    {
        var user = httpContextAccessor.HttpContext?.User;

        var members = await dbContext.Members.ToListAsync(cancellationToken);

        return TypedResults.Ok(new Contracts.Response
        {
            Members = mapper.Map<List<Member>>(members)
        });
    }
}