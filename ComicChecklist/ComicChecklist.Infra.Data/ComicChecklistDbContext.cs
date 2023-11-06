using ComicChecklist.Infra.Data.Configurations;
using ComicChecklist.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace ComicChecklist.Infra.Data
{
    public class ComicChecklistDbContext : DbContext
    {
        public DbSet<Checklist> Checklists { get; set; }

        public DbSet<Issue> Issues { get; set; }

        public ComicChecklistDbContext(DbContextOptions<ComicChecklistDbContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ChecklistConfiguration());
            modelBuilder.ApplyConfiguration(new IssueConfiguration());
        }
    }
}
