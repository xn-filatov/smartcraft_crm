using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SmartCraft.Core.Entities;

namespace SmartCraft.Infrastructure.Data.Configuration.Config;

/// <summary>
/// <see cref="IEntityTypeConfiguration{Interaction}"/> entity configuration
/// </summary>
public class InteractionConfiguration : IEntityTypeConfiguration<Interaction>
{
    /// <inheritdoc/>
    public void Configure(EntityTypeBuilder<Interaction> builder)
    {
        builder.ToTable("interactions", DataSchemaConstants.DEFAULT_SCHEMA_NAME);
        builder.HasKey(p => p.Id);
        builder.Property(p => p.Id)
                .UseHiLo("interactions_seq", DataSchemaConstants.DEFAULT_SCHEMA_NAME);

        builder
              .Property(p => p.Created)
              .HasField("_creationDate")
              .HasColumnName("creation_date")
              .UsePropertyAccessMode(PropertyAccessMode.PreferFieldDuringConstruction)
              .HasDefaultValueSql("now()")
              .IsRequired();
    }
}
