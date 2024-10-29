using CRMProject.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMProject.Persistance.EntityFrameworks.DbConfigurations
{
    public class UserConfig : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name)
                .IsRequired()
                .HasColumnName("Name")
                .HasMaxLength(50);

            builder.HasIndex(x => x.Email)
                .IsUnique();
            builder.Property(x => x.Email)
                .IsRequired();

            builder.Property(x => x.PasswordSalt)
                .IsRequired();
            builder.Property(x=>x.PasswordHash)
                .IsRequired();
            builder.Property(x=>x.Role)
                .IsRequired()
                .HasColumnName("Role")
                .HasMaxLength(50);


        }
    }
}
