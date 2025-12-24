using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SmartCraft.Core.Entities;

namespace SmartCraft.Infrastructure.Data.Configuration.Config;

/// <summary>
/// <see cref="IEntityTypeConfiguration{Contact}"/> entity configuration
/// </summary>
public class ContactConfiguration : IEntityTypeConfiguration<Contact>
{
    /// <inheritdoc/>
    public void Configure(EntityTypeBuilder<Contact> builder)
    {
        builder.ToTable("contacts", DataSchemaConstants.DEFAULT_SCHEMA_NAME);
        builder.HasKey(p => p.Id);
        builder.Property(p => p.Id)
                .UseHiLo("contacts_seq", DataSchemaConstants.DEFAULT_SCHEMA_NAME);

        builder
          .Property(p => p.Created)
          .HasField("_creationDate")
          .HasColumnName("creation_date")
          .UsePropertyAccessMode(PropertyAccessMode.PreferFieldDuringConstruction)
          .HasDefaultValueSql("now()")
          .IsRequired();

        builder.HasMany(e => e.Interactions)
            .WithOne(e => e.Contact)
            .HasForeignKey(e => e.ContactId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);
    }
}
