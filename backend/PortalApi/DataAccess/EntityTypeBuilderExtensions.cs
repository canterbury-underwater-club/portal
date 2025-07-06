using CanterburyUnderwater.PortalApi.DataAccess.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CanterburyUnderwater.PortalApi.DataAccess;

public static class EntityTypeBuilderExtensions
{
    public static void HasEntityDefaults<TEntity>(this EntityTypeBuilder<TEntity> entity)
        where TEntity : EntityDefaults
    {
        entity.HasKey(e => new { e.Id });
        entity.Property(e => e.Id).ValueGeneratedOnAdd();
    }
}