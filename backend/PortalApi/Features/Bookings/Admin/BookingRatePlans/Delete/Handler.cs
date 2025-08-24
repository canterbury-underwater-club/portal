using CanterburyUnderwater.Endpoints;
using CanterburyUnderwater.ErrorHandling;
using CanterburyUnderwater.PortalApi.DataAccess;
using CanterburyUnderwater.PortalApi.Features.Bookings.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace CanterburyUnderwater.PortalApi.Features.Bookings.Admin.BookingRatePlans.Delete;

public class Handler(PortalDbContext db)
    : IRequestEndpointHandler<Contracts.Request, Results<ProblemHttpResult, NoContent>>
{
    public async Task<Results<ProblemHttpResult, NoContent>> HandleAsync(Contracts.Request request,
        CancellationToken ct = default)
    {
        var ratePlan = await db.BookingRatePlans.FindAsync([request.RatePlanId], ct);

        if (ratePlan == null) return ProblemTypedResults.NotFound<BookingRatePlanModel>(request.RatePlanId);

        var totalPlans = await db.BookingRatePlans.CountAsync(ct);
        if (totalPlans <= 1)
            return ProblemTypedResults.Conflict("Cannot delete the last rate plan",
                "At least one booking rate plan must exist at all times.");

        var inUse = await db.Bookings
            .AsNoTracking()
            .AnyAsync(b => b.BookingRatePlanId == request.RatePlanId, ct);

        if (inUse)
            return ProblemTypedResults.Conflict(
                "Rate plan is in use",
                "This rate plan is referenced by existing bookings and cannot be deleted.");


        db.BookingRatePlans.Remove(ratePlan);
        await db.SaveChangesAsync(ct);

        return TypedResults.NoContent();
    }
}