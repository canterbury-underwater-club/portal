using AutoMapper;
using CanterburyUnderwater.Endpoints;
using CanterburyUnderwater.PortalApi.DataAccess;
using CanterburyUnderwater.PortalApi.Features.Bookings.Admin.BookingContractHolders.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace CanterburyUnderwater.PortalApi.Features.Bookings.Admin.BookingContractHolders.List;

public class Handler(PortalDbContext db, IMapper mapper)
    : IResponseEndpointHandler<Ok<Contracts.Response>>
{
    public async Task<Ok<Contracts.Response>> HandleAsync(CancellationToken ct = default)
    {
        var contractHolders = await db.BookingContractHolders.ToListAsync(ct);

        var response = new Contracts.Response
        {
            ContractHolders = mapper.Map<List<BookingContractHolderModel>>(contractHolders)
        };

        return TypedResults.Ok(response);
    }
}