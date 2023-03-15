using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Portfolio.Domain.Entities.Company;

namespace Portfolio.Data.Mapping.Company
{
    public class CompanyMap : IEntityTypeConfiguration<CompanyEntity>
    {
        public void Configure(EntityTypeBuilder<CompanyEntity> builder)
        {
            builder.ToTable("Company");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Id).HasColumnName("IdCompany");

            builder.Property(p => p.Code).IsRequired();

            builder.Property(p => p.Name).IsRequired().HasMaxLength(120);

            builder.Property(p => p.DateOfBirth);
        }
    }
}