using EthioClass.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EthioClass.Infrastructure.Data.Configurations;

public class SchoolConfiguration : IEntityTypeConfiguration<School>
{
    public void Configure(EntityTypeBuilder<School> builder)
    {
        builder.HasKey(s => s.Id);
        builder.Property(s => s.Name).IsRequired().HasMaxLength(200);
        builder.Property(s => s.NameAmharic).IsRequired().HasMaxLength(200);
        builder.Property(s => s.Code).IsRequired().HasMaxLength(20);
        builder.Property(s => s.Email).HasMaxLength(200);
        builder.Property(s => s.PhoneNumber).HasMaxLength(20);
        builder.Property(s => s.Website).HasMaxLength(200);

        builder.Property(s => s.Address).HasMaxLength(300);
        builder.Property(s => s.City).HasMaxLength(100);
        builder.Property(s => s.Region).HasMaxLength(100);

        builder.Property(s => s.LogoUrl).HasMaxLength(500);
        builder.Property(s => s.Motto).HasMaxLength(300);

        builder.HasIndex(s => s.Code).IsUnique();
    }
}