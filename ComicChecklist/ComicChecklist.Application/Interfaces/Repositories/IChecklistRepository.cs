using ComicChecklist.Domain.Models;

namespace ComicChecklist.Application.Interfaces.Repositories
{
    public interface IChecklistRepository : IGenericRepository<Checklist>
    {
        Task<IEnumerable<Checklist>> GetAvailableChecklistsAsync(int userId);
        Task<IEnumerable<Checklist>> SearchAsync(string name, int skip, int take);        
    }
}