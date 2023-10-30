using ComicChecklist.Domain.Models;

namespace ComicChecklist.Data.Repositories
{
    public class ChecklistRepository : GenericRepository<Checklist>, IChecklistRepository
    {
        public ChecklistRepository(ComicChecklistDbContext dbContext) : base(dbContext)
        {
        }
    }
}
