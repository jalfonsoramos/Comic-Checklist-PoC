using ComicChecklist.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ComicChecklist.Infra.Data.Configurations
{
    internal class UserIssueStatusConfiguration : IEntityTypeConfiguration<UserIssueStatus>
    {
        public void Configure(EntityTypeBuilder<UserIssueStatus> builder)
        {
            builder.ToTable("UserIssueStatuses");
            builder.HasKey(x => new { x.UserId, x.IssueId });
            builder.Property(x => x.UserId).IsRequired();
            builder.Property(x => x.IssueId).IsRequired();
            builder.Property(x => x.Status).HasConversion<int>();

            builder.HasOne(x => x.User)
                   .WithMany(x => x.UserIssueStatuses)
                   .HasForeignKey(x => x.UserId);
            builder.HasOne(x => x.Issue)
                   .WithMany(x => x.UserIssueStatuses)
                   .HasForeignKey(x => x.IssueId);
        }
    }
}
