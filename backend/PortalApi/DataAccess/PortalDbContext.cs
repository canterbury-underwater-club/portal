using CanterburyUnderwater.PortalApi.Authorization;
using CanterburyUnderwater.PortalApi.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace CanterburyUnderwater.PortalApi.DataAccess;

public class PortalDbContext : DbContext
{
    public PortalDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Role> Roles { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseLazyLoadingProxies();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>(entity => { entity.HasEntityDefaults(); });

        modelBuilder.Entity<Role>().HasData(
            new Role { Id = RoleIds.Admin, Name = RoleNames.Admin },
            new Role { Id = RoleIds.Committee, Name = RoleNames.Committee }
        );
    }

    public override int SaveChanges(bool acceptAllChangesOnSuccess)
    {
        SetEntityDetails();
        return base.SaveChanges(acceptAllChangesOnSuccess);
    }

    public override async Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess,
        CancellationToken cancellationToken = default)
    {
        SetEntityDetails();
        return await base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
    }

    private void SetEntityDetails()
    {
        var entries = ChangeTracker.Entries().Where(e => e.State is EntityState.Modified or EntityState.Added);
        foreach (var entry in entries)
            if (entry.Entity is EntityDefaults entity)
            {
                if (entry.State == EntityState.Added)
                    entity.CreatedAt = DateTime.UtcNow;

                entity.UpdatedAt = DateTime.UtcNow;
                entity.UpdatedBy = Guid.Empty;
            }
    }
}