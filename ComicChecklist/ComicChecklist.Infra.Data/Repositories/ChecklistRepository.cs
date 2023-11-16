using ComicChecklist.Application.Interfaces.Repositories;
using ComicChecklist.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace ComicChecklist.Infra.Data.Repositories
{
    public class ChecklistRepository : GenericRepository<Checklist>, IChecklistRepository
    {
        public ChecklistRepository(ComicChecklistDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<IEnumerable<Checklist>> GetAvailableChecklists(int userId)
        {
            return await DbContext.Checklists
                                  .Include(x=>x.Issues)
                                  .AsNoTracking()
                                  .Where(c => !DbContext.UserChecklists
                                  .Where(uc => uc.UserId == userId)
                                  .Select(uc => uc.ChecklistId)
                                  .Contains(c.Id))
                                  .ToListAsync();
        }

        public override async Task<Checklist> GetByIdAsync(int id)
        {
            return await DbContext.Checklists.Include(x => x.Issues).SingleOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<Checklist>> Search(string name, int skip, int take)
        {
            if (string.IsNullOrEmpty(name))
            {
                return await DbContext.Checklists.Include(x => x.Issues)
                                                 .OrderBy(x => x.Name)
                                                 .Skip(skip)
                                                 .Take(take)
                                                 .ToListAsync();
            }
            else
            {
                return await DbContext.Checklists.Where(x => x.Name.Contains(name))
                                                 .Include(x => x.Issues)
                                                 .OrderBy(x => x.Name)
                                                 .Skip(skip)
                                                 .Take(take)
                                                 .ToListAsync();
            }
        }
    }
}
