using System.ComponentModel.DataAnnotations;

namespace CanterburyUnderwater.PortalApi.DataAccess.Entities;

public class Role
{
    public Guid Id { get; set; }
    [MaxLength(64)] public required string Name { get; set; }

    public virtual ICollection<User> Users { get; set; } = [];
}