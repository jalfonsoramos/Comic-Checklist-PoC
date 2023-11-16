﻿using ComicChecklist.Domain.Models;

namespace ComicChecklist.Application.Interfaces.Repositories
{
    public interface IUserRepository : IGenericRepository<User>
    {
        Task<int> GetUserByName(string username);
    }
}