using ComicChecklist.Domain.Models;

namespace ComicChecklist.Data.Repositories
{
    public interface IChecklistRepository : IGenericRepository<Checklist>
    {
        Task<IEnumerable<Checklist>> Search(string name, int skip, int take);
    }
}