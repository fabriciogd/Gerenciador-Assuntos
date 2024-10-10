using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Topic.Domain.Entities;

namespace Topic.Persistence.Configurations;

/// <summary>
/// Configures the entity properties and constraints for the <see cref="NewsletterLink"/> entity.
/// Implements the <see cref="IEntityTypeConfiguration{NewsletterLink}"/> interface, 
/// used by Entity Framework to configure the model via the Fluent API.
/// </summary>
internal class NewsletterLinkConfiguration : IEntityTypeConfiguration<NewsletterLink>
{
    public void Configure(EntityTypeBuilder<NewsletterLink> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .ValueGeneratedNever();

        builder.Property(x => x.Title)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(x => x.Description)
            .HasMaxLength(500)
            .IsRequired();

        builder.Property(x => x.Url)
            .HasMaxLength(2083)
            .IsRequired();

        builder.HasOne(x => x.Newsletter)
            .WithMany(x => x.Links)
            .HasForeignKey(x => x.NewsletterId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}
