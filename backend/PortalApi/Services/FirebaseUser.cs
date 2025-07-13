using System.Security.Claims;

namespace CanterburyUnderwater.PortalApi.Services;

public class FirebaseUser
{
    private FirebaseUser()
    {
    }

    public required string UserId { get; init; }
    public required string EmailAddress { get; init; }
    public required string FirstName { get; init; }
    public string? LastName { get; init; }
    public string? PhotoUrl { get; init; }

    public static FirebaseUser? FromClaimsIdentity(ClaimsIdentity? user)
    {
        if (user?.IsAuthenticated != true)
            throw new UnauthorizedAccessException("User is not authenticated.");

        var userId = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var emailAddress = user.FindFirst(ClaimTypes.Email)?.Value;
        var name = user.FindFirst("name")?.Value;
        var photoUrl = user.FindFirst("picture")?.Value;

        if (string.IsNullOrEmpty(userId))
            throw new UnauthorizedAccessException($"Missing required {ClaimTypes.NameIdentifier} claim.");
        if (string.IsNullOrEmpty(emailAddress))
            throw new UnauthorizedAccessException($"Missing required {ClaimTypes.Email} claim.");
        if (string.IsNullOrEmpty(name))
            throw new UnauthorizedAccessException("Missing required name claim.");

        var (firstName, lastName) = SplitFullName(name);

        return new FirebaseUser
        {
            UserId = userId,
            EmailAddress = emailAddress,
            FirstName = firstName,
            LastName = lastName,
            PhotoUrl = photoUrl
        };
    }

    private static (string FirstName, string? LastName) SplitFullName(string fullName)
    {
        if (string.IsNullOrWhiteSpace(fullName))
            return (string.Empty, null);

        var parts = fullName.Trim().Split(' ', StringSplitOptions.RemoveEmptyEntries);
        if (parts.Length == 1)
            return (parts[0], null); // Only one name, treat as first name

        // Combine all but last as first name, last part as last name
        var firstName = string.Join(" ", parts.Take(parts.Length - 1));
        var lastName = parts.Last();
        return (firstName, lastName);
    }
}