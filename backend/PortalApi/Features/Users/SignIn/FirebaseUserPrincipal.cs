using System.Security.Claims;

namespace CanterburyUnderwater.PortalApi.Features.Users.SignIn;

public class FirebaseUserPrincipal
{
    private FirebaseUserPrincipal()
    {
    }

    public required string UserId { get; init; }
    public required string EmailAddress { get; init; }
    public required string FirstName { get; init; }
    public required string LastName { get; init; }

    public static FirebaseUserPrincipal? FromClaimsPrincipal(ClaimsPrincipal? user)
    {
        if (user?.Identity?.IsAuthenticated != true)
            throw new UnauthorizedAccessException("User is not authenticated.");

        var userId = user.FindFirstValue(ClaimTypes.NameIdentifier);
        var emailAddress = user.FindFirstValue(ClaimTypes.Email);
        var name = user.FindFirstValue("name");

        if (string.IsNullOrEmpty(userId))
            throw new UnauthorizedAccessException($"Missing required {ClaimTypes.NameIdentifier} claim.");
        if (string.IsNullOrEmpty(emailAddress))
            throw new UnauthorizedAccessException($"Missing required {ClaimTypes.Email} claim.");
        if (string.IsNullOrEmpty(name))
            throw new UnauthorizedAccessException("Missing required name claim.");

        var (firstName, lastName) = SplitFullName(name);

        return new FirebaseUserPrincipal
        {
            UserId = userId,
            EmailAddress = emailAddress,
            FirstName = firstName,
            LastName = lastName
        };
    }

    private static (string FirstName, string LastName) SplitFullName(string fullName)
    {
        if (string.IsNullOrWhiteSpace(fullName))
            return (string.Empty, string.Empty);

        var parts = fullName.Trim().Split(' ', StringSplitOptions.RemoveEmptyEntries);
        if (parts.Length == 1)
            return (parts[0], string.Empty); // Only one name, treat as first name

        // Combine all but last as first name, last part as last name
        var firstName = string.Join(" ", parts.Take(parts.Length - 1));
        var lastName = parts.Last();
        return (firstName, lastName);
    }
}