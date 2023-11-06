using ComicChecklist.Domain.Interfaces.Repositories;
using ComicChecklist.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace ComicChecklist.Data.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : Entity
    {
        protected ComicChecklistDbContext DbContext;

        public GenericRepository(ComicChecklistDbContext dbContext)
        {
            DbContext = dbContext;
        }

        public void Add(T entity)
        {
            DbContext.Set<T>().Add(entity);
        }

        public virtual async Task<T> GetByIdAsync(int id)
        {
            return await DbContext.Set<T>().SingleOrDefaultAsync(x => x.Id == id);
        }

        public async Task SaveChangesAsync()
        {
            await DbContext.SaveChangesAsync();
        }
    }
}
