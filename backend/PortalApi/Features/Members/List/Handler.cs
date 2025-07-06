using CanterburyUnderwater.Endpoints;
using CanterburyUnderwater.PortalApi.Features.Members.Models;
using Microsoft.AspNetCore.Http.HttpResults;

namespace CanterburyUnderwater.PortalApi.Features.Members.List;

public class Handler : IResponseEndpointHandler<Ok<Contracts.Response>>
{
    public async Task<Ok<Contracts.Response>> HandleAsync(CancellationToken cancellationToken = default)
    {
        await Task.Delay(1, cancellationToken);
        return TypedResults.Ok(new Contracts.Response
        {
            Members =
            [
                new Member
                {
                    Id = Guid.NewGuid(),
                    FirstName = "John",
                    LastName = "Doe"
                },
                new Member
                {
                    Id = Guid.NewGuid(),
                    FirstName = "Jane",
                    LastName = "Doe"
                }
            ]
        });
    }
}