using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Topic.Domain.Entities;

namespace Topic.Persistence.Configurations;

/// <summary>
/// Configures the entity properties and constraints for the <see cref="Newsletter"/> entity.
/// Implements the <see cref="IEntityTypeConfiguration{Newsletter}"/> interface, 
/// used by Entity Framework to configure the model via the Fluent API.
/// </summary>
internal class NewsletterConfiguration : IEntityTypeConfiguration<Newsletter>
{
    public void Configure(EntityTypeBuilder<Newsletter> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .ValueGeneratedNever();

        builder.Property(x => x.Title)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(x => x.Keywords)
            .IsRequired();

        builder.Property(x => x.CreatedOnUtc)
            .IsRequired();

        builder.Property(attendee => attendee.ModifiedOnUtc);

        builder.Property(x => x.LinksCount)
            .IsRequired()
            .HasDefaultValue(0);
    }
}
