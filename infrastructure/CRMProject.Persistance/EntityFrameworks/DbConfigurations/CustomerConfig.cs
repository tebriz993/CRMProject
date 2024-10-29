using CRMProject.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CRMProject.Persistance.EntityFrameworks.DbConfigurations
{
    public class CustomerConfig : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.CompanyName)
                .IsRequired()
                .HasColumnName("CompanyName")
                .HasMaxLength(100);

            builder.Property(x => x.ContactPersonName)
                .IsRequired()
                .HasColumnName("ContactPersonName")
                .HasMaxLength(50);

            builder.Property(x => x.Phone)
                .IsRequired()
                .HasColumnName("Phone")
                .HasMaxLength(20);

        }
    }
}
