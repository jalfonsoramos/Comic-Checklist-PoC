﻿using ComicChecklist.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ComicChecklist.Infra.Data.Configurations
{
    internal class ChecklistConfiguration : IEntityTypeConfiguration<Checklist>
    {
        public void Configure(EntityTypeBuilder<Checklist> builder)
        {
            builder.ToTable("Checklists");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnName("ChecklistId");
            builder.Property(x => x.Name).HasMaxLength(255).IsRequired();
            builder.Property(x => x.Clocked).HasDefaultValue(false);
            builder.Property(x => x.Createad).HasComputedColumnSql("GETUTCDATE()");

            builder.HasMany(e => e.Issues)
                    .WithOne(e => e.Checklist)
                    .HasForeignKey(e => e.ChecklistId)
                    .HasPrincipalKey(e => e.Id);
        }
    }
}
