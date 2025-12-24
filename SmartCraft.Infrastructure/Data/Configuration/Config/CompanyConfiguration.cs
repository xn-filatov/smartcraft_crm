using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SmartCraft.Core.Entities;
using SmartCraft.Core.Entities.Enums;

namespace SmartCraft.Infrastructure.Data.Configuration.Config;

/// <summary>
/// <see cref="IEntityTypeConfiguration{Company}"/> entity configuration
/// </summary>
public class CompanyConfiguration : IEntityTypeConfiguration<Company>
{
    /// <inheritdoc/>
    public void Configure(EntityTypeBuilder<Company> builder)
    {
        builder.ToTable("company", DataSchemaConstants.DEFAULT_SCHEMA_NAME);
        builder.HasKey(p => p.Id);
        builder.Property(p => p.Id)
                .UseHiLo("company_seq", DataSchemaConstants.DEFAULT_SCHEMA_NAME);

        builder.Property(e => e.Size)
            .HasConversion(
                v => v.ToString(),
                v => (CompanySize)Enum.Parse(typeof(CompanySize), v));

        builder.HasMany(e => e.Contacts)
            .WithOne(i => i.Company)
            .HasForeignKey(e => e.CompanyId)
            .OnDelete(DeleteBehavior.Cascade);

        builder
            .Property(p => p.Created)
            .HasField("_creationDate")
            .HasColumnName("creation_date")
            .UsePropertyAccessMode(PropertyAccessMode.PreferFieldDuringConstruction)
            .HasDefaultValueSql("now()")
            .IsRequired();
    }
}
