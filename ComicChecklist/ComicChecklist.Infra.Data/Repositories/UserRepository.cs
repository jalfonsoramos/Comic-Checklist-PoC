using ComicChecklist.Application.Interfaces.Repositories;
using ComicChecklist.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace ComicChecklist.Infra.Data.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(ComicChecklistDbContext dbContext) : base(dbContext)
        {

        }

        public async Task<int> GetUserIdByNameAsync(string username)
        {
            var user = await DbContext.Users.AsNoTracking().SingleAsync(x => x.UserName == username);
            return user.Id;
        }
    }
}
