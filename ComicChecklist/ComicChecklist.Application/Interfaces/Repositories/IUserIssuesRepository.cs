using ComicChecklist.Domain.Enums;

namespace ComicChecklist.Application.Interfaces.Repositories
{
    public interface IUserIssuesRepository : IRepository
    {
        Task CreateUserIssueStatusAsync(int userId, int issueId, IssueStatus issueStatus);
        Task<bool> IssueStatusExistAsync(int userId, int issueId);        
        Task UpdateUserIssueStatusAsync(int userId, int issueId, IssueStatus issueStatus);
    }
}