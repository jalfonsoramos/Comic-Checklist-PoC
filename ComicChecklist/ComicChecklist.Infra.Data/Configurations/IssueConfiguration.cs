using ComicChecklist.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ComicChecklist.Infra.Data.Configurations
{
    internal class IssueConfiguration : IEntityTypeConfiguration<Issue>
    {
        public void Configure(EntityTypeBuilder<Issue> builder)
        {
            builder.ToTable("Issues");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnName("IssueId");
            builder.Property(x => x.Order).IsRequired();
            builder.Property(x => x.Title).HasMaxLength(255).IsRequired();
            builder.Property(x => x.Clocked).HasDefaultValue(false);
            builder.Property(x => x.Createad).HasComputedColumnSql("GetUtcDate()");
        }
    }
}
