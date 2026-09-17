using ComicChecklist.Domain.Models;

namespace ComicChecklist.Application.Interfaces.Repositories
{
    public interface IGenericRepository<T> : IRepository where T : Entity
    {
        void Add(T entity);
        Task<T> GetByIdAsync(int id);
    }
}