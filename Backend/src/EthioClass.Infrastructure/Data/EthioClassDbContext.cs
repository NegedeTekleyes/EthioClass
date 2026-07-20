using EthioClass.Application.Common.Interfaces;
using EthioClass.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EthioClass.Infrastructure.Data;

public class EthioClassDbContext(DbContextOptions<EthioClassDbContext> options)
    : DbContext(options), IApplicationDbContext
{
    public DbSet<School> Schools => Set<School>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(EthioClassDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }

    public override Task<int> SaveChangesAsync(CancellationToken ct = default)
    {
        foreach (var entry in ChangeTracker.Entries<School>())
        {
            if (entry.State == EntityState.Modified)
                entry.Entity.UpdatedAt = DateTime.UtcNow;
        }
        return base.SaveChangesAsync(ct);
    }
}