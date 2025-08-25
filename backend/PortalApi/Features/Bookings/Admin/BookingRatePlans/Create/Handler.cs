using AutoMapper;
using CanterburyUnderwater.PortalApi.DataAccess;
using CanterburyUnderwater.PortalApi.DataAccess.Entities;
using CanterburyUnderwater.PortalApi.EndpointHandling;
using CanterburyUnderwater.PortalApi.Features.Bookings.Models;
using Microsoft.AspNetCore.Http.HttpResults;

namespace CanterburyUnderwater.PortalApi.Features.Bookings.Admin.BookingRatePlans.Create;

public class Handler(PortalDbContext db, IMapper mapper) : IRequestEndpointHandler<Contracts.Request,
    Results<ProblemHttpResult, Created<Contracts.Response>>>
{
    public async Task<Results<ProblemHttpResult, Created<Contracts.Response>>> HandleAsync(Contracts.Request request,
        CancellationToken ct = default)
    {
        var ratePlan = mapper.Map<BookingRatePlan>(request);

        db.BookingRatePlans.Add(ratePlan);

        await db.SaveChangesAsync(ct);

        var response = new Contracts.Response
        {
            RatePlan = mapper.Map<BookingRatePlanModel>(ratePlan)
        };

        return TypedResults.Created($"/bookings/admin/rate-plans/{ratePlan.Id}", response);
    }
}