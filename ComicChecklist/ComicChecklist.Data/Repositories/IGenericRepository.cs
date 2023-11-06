﻿using ComicChecklist.Domain.Models;

namespace ComicChecklist.Data.Repositories
{
    public interface IGenericRepository<T> where T : Entity
    {
        void Add(T entity);
        Task<T> GetByIdAsync(int id);
        Task SaveChangesAsync();
    }
}