using ComicChecklist.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ComicChecklist.Infra.Data.Configurations
{
    internal class UserChecklistConfiguration : IEntityTypeConfiguration<UserChecklist>
    {
        public void Configure(EntityTypeBuilder<UserChecklist> builder)
        {
            builder.ToTable("UserChecklists");
            builder.HasKey(x => new { x.UserId, x.ChecklistId });
            builder.Property(x => x.UserId).IsRequired();
            builder.Property(x => x.ChecklistId).IsRequired();

            builder.HasOne(x => x.User)
                   .WithMany(x => x.UserChecklists)
                   .HasForeignKey(x => x.UserId);
            builder.HasOne(x => x.Checklist)
                   .WithMany(x => x.UserChecklists)
                   .HasForeignKey(x => x.UserId);
        }
    }
}
