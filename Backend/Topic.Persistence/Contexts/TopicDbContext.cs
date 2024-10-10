using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Topic.Domain.Abstractions;
using Topic.Persistence.Configurations;

namespace Topic.Persistence.Contexts;

/// <summary>
/// The TopicContext class represents the Entity Framework database context for the application.
/// It inherits from <see cref="DbContext"/> and is configured with the provided options for database connection.
/// This class is responsible for managing the interaction with the database, including the definition of the models (entities) and their configurations.
/// </summary>
/// <param name="dbOptions">The options to configure the DbContext, such as the connection string and provider.</param>
public class TopicDbContext: DbContext
{
    public TopicDbContext(DbContextOptions options) : base(options)
    {
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfiguration(new NewsletterConfiguration());
        modelBuilder.ApplyConfiguration(new NewsletterLinkConfiguration());
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        DateTime utcNow = DateTime.UtcNow;

        UpdateAuditableEntities(utcNow);

        return await base.SaveChangesAsync(cancellationToken);
    }

    private void UpdateAuditableEntities(DateTime utcNow)
    {
        foreach (EntityEntry<IAuditableEntity> entityEntry in ChangeTracker.Entries<IAuditableEntity>())
        {
            if (entityEntry.State == EntityState.Added)
            {
                entityEntry.Property(nameof(IAuditableEntity.CreatedOnUtc)).CurrentValue = utcNow;
            }

            if (entityEntry.State == EntityState.Modified)
            {
                entityEntry.Property(nameof(IAuditableEntity.ModifiedOnUtc)).CurrentValue = utcNow;
            }
        }
    }
}
