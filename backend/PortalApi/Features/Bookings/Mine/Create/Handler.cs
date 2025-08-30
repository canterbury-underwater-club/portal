using CanterburyUnderwater.PortalApi.EndpointHandling;
using Microsoft.AspNetCore.Http.HttpResults;

namespace CanterburyUnderwater.PortalApi.Features.Bookings.Mine.Create;

public class Handler : IRequestEndpointHandler<Contracts.Request,
    Results<ProblemHttpResult, ValidationProblem, Created<Contracts.Response>>>
{
    public Task<Results<ProblemHttpResult, ValidationProblem, Created<Contracts.Response>>> HandleAsync(
        Contracts.Request request, CancellationToken ct = default)
    {
        throw new NotImplementedException();
    }
}