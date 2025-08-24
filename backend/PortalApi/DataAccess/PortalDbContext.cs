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

    // === Bookings ===
    public DbSet<Booking> Bookings => Set<Booking>();
    public DbSet<BookingAttendee> BookingAttendees => Set<BookingAttendee>();
    public DbSet<BookingContractHolder> BookingContractHolders => Set<BookingContractHolder>();
    public DbSet<BookingRatePlan> BookingRatePlans => Set<BookingRatePlan>();
    public DbSet<BookingRate> BookingRates => Set<BookingRate>();
    public DbSet<BookingFee> BookingFees => Set<BookingFee>();

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

        // === Bookings ===
        modelBuilder.Entity<Booking>(e =>
        {
            e.HasEntityDefaults();

            e.HasOne(b => b.PrimaryContact)
                .WithMany()
                .HasForeignKey(b => b.PrimaryContactId)
                .OnDelete(DeleteBehavior.Restrict);

            e.HasOne(b => b.BookingRatePlan)
                .WithMany()
                .HasForeignKey(b => b.BookingRatePlanId)
                .OnDelete(DeleteBehavior.Restrict);

            e.HasOne(b => b.ContractHolder)
                .WithMany()
                .HasForeignKey(b => b.ContractHolderId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<BookingAttendee>(e =>
        {
            e.HasEntityDefaults();

            e.HasOne(a => a.Booking)
                .WithMany(b => b.Attendees)
                .HasForeignKey(a => a.BookingId)
                .OnDelete(DeleteBehavior.Cascade);

            e.HasOne(a => a.LinkedUser)
                .WithMany()
                .HasForeignKey(a => a.LinkedUserId)
                .OnDelete(DeleteBehavior.SetNull);
        });

        modelBuilder.Entity<BookingContractHolder>(e =>
        {
            e.HasEntityDefaults();
            e.HasIndex(ch => new { ch.Name }).IsUnique();
        });

        modelBuilder.Entity<BookingRatePlan>(e =>
        {
            e.HasEntityDefaults();

            e.HasMany(p => p.Rates)
                .WithOne(r => r.BookingRatePlan)
                .HasForeignKey(r => r.BookingRatePlanId)
                .OnDelete(DeleteBehavior.Cascade);

            e.HasMany(p => p.Fees)
                .WithOne(f => f.BookingRatePlan)
                .HasForeignKey(f => f.BookingRatePlanId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<BookingRate>(e =>
        {
            e.HasEntityDefaults();

            e.HasOne(r => r.ContractHolder)
                .WithMany()
                .HasForeignKey(r => r.ContractHolderId)
                .OnDelete(DeleteBehavior.Restrict);

            e.HasIndex(r => new { r.BookingRatePlanId, r.RateType, r.AttendeeType, r.AgeBracket })
                .IsUnique();

            e.HasIndex(r => new { r.BookingRatePlanId, r.RateType, r.ContractHolderId })
                .IsUnique();
        });

        modelBuilder.Entity<BookingFee>(e =>
        {
            e.HasEntityDefaults();

            e.HasIndex(f => new { f.BookingRatePlanId, f.Name }).IsUnique();
        });
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