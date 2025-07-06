using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;

namespace CanterburyUnderwater.ErrorHandling;

public static class ProblemTypedResults
{
    public static ProblemHttpResult BadRequest(string? reason = null)
    {
        return TypedResults.Problem(reason, statusCode: StatusCodes.Status400BadRequest);
    }

    public static ProblemHttpResult NotFound()
    {
        return TypedResults.Problem(statusCode: StatusCodes.Status404NotFound);
    }

    public static ProblemHttpResult NotFound<T>(Guid? id)
    {
        return id.HasValue ? NotFound<T>(id.Value.ToString()) : NotFound<T>();
    }

    public static ProblemHttpResult NotFound<T>(string? key = null)
    {
        var sb = new StringBuilder($"{typeof(T).Name} not found");
        if (key != null) sb.Append($", Key: {key}");
        return TypedResults.Problem(sb.ToString(), statusCode: StatusCodes.Status404NotFound);
    }

    public static ProblemHttpResult Conflict()
    {
        return TypedResults.Problem(statusCode: StatusCodes.Status409Conflict);
    }

    public static ProblemHttpResult Forbidden(string? reason = null)
    {
        return TypedResults.Problem(reason, statusCode: StatusCodes.Status403Forbidden);
    }
}