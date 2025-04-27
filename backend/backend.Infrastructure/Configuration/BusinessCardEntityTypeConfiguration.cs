using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Configuration
{
    public class BusinessCardEntityTypeConfiguration : IEntityTypeConfiguration<BusinessCard>
    {
        public void Configure(EntityTypeBuilder<BusinessCard> builder)
        {
            builder.Property(s => s.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(s => s.Gender)
                .IsRequired()
                .HasConversion<string>();

            builder.Property(s => s.DateOfBirth)
              .IsRequired();

            builder.Property(s => s.Email)
              .IsRequired()
              .HasMaxLength(100);

            builder.Property(s => s.Phone)
              .IsRequired()
              .HasMaxLength(20);

            builder.Property(s => s.Address)
              .IsRequired()
              .HasMaxLength(200);

        }
    }
}
