using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace CanterburyUnderwater.ErrorHandling.Extensions;

public static class RouteHandlerBuilderExtensions
{
    public static RouteHandlerBuilder ProducesBadRequestProblem(
        this RouteHandlerBuilder builder,
        int statusCode = StatusCodes.Status400BadRequest,
        string? contentType = null)
    {
        return builder.ProducesProblem(statusCode, contentType);
    }

    public static RouteHandlerBuilder ProducesForbiddenProblem(
        this RouteHandlerBuilder builder,
        int statusCode = StatusCodes.Status403Forbidden,
        string? contentType = null)
    {
        return builder.ProducesProblem(statusCode, contentType);
    }

    public static RouteHandlerBuilder ProducesNotFoundProblem(
        this RouteHandlerBuilder builder,
        int statusCode = StatusCodes.Status404NotFound,
        string? contentType = null)
    {
        return builder.ProducesProblem(statusCode, contentType);
    }

    public static RouteHandlerBuilder ProducesConflictProblem(
        this RouteHandlerBuilder builder,
        int statusCode = StatusCodes.Status409Conflict,
        string? contentType = null)
    {
        return builder.ProducesProblem(statusCode, contentType);
    }
}