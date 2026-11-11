using DalilAcademy.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace DalilAcademy.Infra.Data.Configuration
{
    internal class StudentConfiguration : IEntityTypeConfiguration<Student>
    {
        public void Configure(EntityTypeBuilder<Student> builder)
        {
            builder.HasKey(s => s.Id);
            builder.Property(s => s.FullName)
                   .IsRequired()
                   .HasMaxLength(100);
            builder.Property(s => s.Email)
                   .IsRequired()
                   .HasMaxLength(255);
                   builder.HasIndex(s => s.Email)
                   .IsUnique();
            builder.HasMany(s => s.Enrollments)
               .WithOne(e => e.Student)
               .HasForeignKey(e => e.StudentId);
            builder.Property(s => s.PhoneNumber)
                   .IsRequired()
                   .HasMaxLength(15);
        }
    }
}
