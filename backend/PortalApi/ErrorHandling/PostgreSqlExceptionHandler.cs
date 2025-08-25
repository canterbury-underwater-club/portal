using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace CanterburyUnderwater.PortalApi.ErrorHandling;

public class PostgreSqlExceptionHandler : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception,
        CancellationToken cancellationToken)
    {
        var result = TryHandleException(exception);
        if (result == null) return false;

        await result.ExecuteAsync(httpContext);

        return true;
    }

    private static ProblemHttpResult? TryHandleException(Exception exception)
    {
        switch (exception)
        {
            case DbUpdateException dbUpdateException:
                if (dbUpdateException.IsUniqueConstraintViolation())
                    return ProblemTypedResults.Conflict();
                break;
        }

        return null;
    }
}