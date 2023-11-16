using ComicChecklist.Domain.Dtos;
using ComicChecklist.Domain.Models;

namespace ComicChecklist.Application.Interfaces.Repositories
{
    public interface IChecklistRepository : IGenericRepository<Checklist>
    {
        Task<IEnumerable<Checklist>> GetAvailableChecklists(int userId);
        Task<IEnumerable<Checklist>> Search(string name, int skip, int take);
    }
}