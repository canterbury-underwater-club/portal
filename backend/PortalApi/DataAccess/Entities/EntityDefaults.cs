namespace CanterburyUnderwater.PortalApi.DataAccess.Entities;

public abstract class EntityDefaults
{
    public Guid Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public Guid UpdatedBy { get; set; }
}