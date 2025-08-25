using Microsoft.EntityFrameworkCore;
using Npgsql;

namespace CanterburyUnderwater.PortalApi.ErrorHandling;

public static class DbUpdateExceptionExtensions
{
    public static bool IsUniqueConstraintViolation(this DbUpdateException exception)
    {
        if (exception.InnerException is PostgresException postgresException)
            // PostgreSQL error code for unique violation
            return postgresException.SqlState == "23505";

        return false;
    }
}