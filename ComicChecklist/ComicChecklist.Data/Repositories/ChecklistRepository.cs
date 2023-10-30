using ComicChecklist.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace ComicChecklist.Data.Repositories
{
    public class ChecklistRepository : GenericRepository<Checklist>, IChecklistRepository
    {
        public ChecklistRepository(ComicChecklistDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<IEnumerable<Checklist>> Search(string name, int skip, int take)
        {
            if (string.IsNullOrEmpty(name))
            {
                return await DbContext.Checklists.OrderBy(x => x.Name)
                                                 .Skip(skip)
                                                 .Take(take)
                                                 .ToListAsync();
            }
            else
            {
                return await DbContext.Checklists.Where(x => x.Name.Contains(name))
                                                 .OrderBy(x => x.Name)
                                                 .Skip(skip)
                                                 .Take(take)
                                                 .ToListAsync();
            }
        }
    }
}
