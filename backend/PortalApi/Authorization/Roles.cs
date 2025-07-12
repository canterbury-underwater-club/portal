namespace CanterburyUnderwater.PortalApi.Authorization;

public static class RoleNames
{
    public const string Admin = "Admin";
    public const string Committee = "Committee";
}

public static class RoleIds
{
    public static readonly Guid Admin = Guid.Parse("7f119827-937c-4546-a576-e9a0118102c5");
    public static readonly Guid Committee = Guid.Parse("249b7d49-5495-4b3f-87ea-2ebb2bbd6910");
}