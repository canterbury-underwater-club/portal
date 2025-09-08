using AutoMapper;
using CanterburyUnderwater.PortalApi.DataAccess;
using CanterburyUnderwater.PortalApi.DataAccess.Entities;
using CanterburyUnderwater.PortalApi.EndpointHandling;
using CanterburyUnderwater.PortalApi.ErrorHandling;
using CanterburyUnderwater.PortalApi.Features.Users.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace CanterburyUnderwater.PortalApi.Features.Users.Create;

public class Handler(PortalDbContext db, IMapper mapper)
    : IRequestEndpointHandler<Contracts.Request,
        Results<ProblemHttpResult, ValidationProblem, Created<Contracts.Response>>>
{
    public async Task<Results<ProblemHttpResult, ValidationProblem, Created<Contracts.Response>>> HandleAsync(
        Contracts.Request request,
        CancellationToken ct = default)
    {
        var userExists = await db.Users
            .AsNoTracking()
            .AnyAsync(u => u.EmailAddress == request.EmailAddress || u.SecondaryEmailAddress == request.EmailAddress,
                ct);

        if (userExists)
            return ProblemTypedResults.Conflict($"User with email {request.EmailAddress} already exists.");

        var user = mapper.Map<User>(request);

        await db.Users.AddAsync(user, ct);
        await db.SaveChangesAsync(ct);

        var response = new Contracts.Response
        {
            User = mapper.Map<UserModel>(user)
        };

        return TypedResults.Created($"/users/{user.Id}", response);
    }
}