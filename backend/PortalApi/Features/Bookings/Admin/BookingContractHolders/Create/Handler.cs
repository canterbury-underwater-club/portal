using AutoMapper;
using CanterburyUnderwater.Endpoints;
using CanterburyUnderwater.PortalApi.DataAccess;
using CanterburyUnderwater.PortalApi.DataAccess.Entities;
using CanterburyUnderwater.PortalApi.Features.Bookings.Admin.BookingContractHolders.Models;
using Microsoft.AspNetCore.Http.HttpResults;

namespace CanterburyUnderwater.PortalApi.Features.Bookings.Admin.BookingContractHolders.Create;

public class Handler(PortalDbContext db, IMapper mapper)
    : IRequestEndpointHandler<Contracts.Request,
        Results<ProblemHttpResult, Created<Contracts.Response>>>
{
    public async Task<Results<ProblemHttpResult, Created<Contracts.Response>>> HandleAsync(
        Contracts.Request request,
        CancellationToken ct = default)
    {
        var contractHolder = mapper.Map<BookingContractHolder>(request);

        db.BookingContractHolders.Add(contractHolder);

        await db.SaveChangesAsync(ct);

        var response = new Contracts.Response
        {
            ContractHolder = mapper.Map<BookingContractHolderModel>(contractHolder)
        };

        return TypedResults.Created($"/bookings/admin/contract-holders/{contractHolder.Id}", response);
    }
}