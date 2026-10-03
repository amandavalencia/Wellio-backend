using Wellio.Models;

namespace Wellio.Repositories.IRepository
{
    public interface IActivityRepo
    {
        Task<List<ActivityEntry>> GetAllAsync(string userId);
        Task<ActivityEntry?> GetByIdAsync(int id, string userId);
        Task<ActivityEntry> CreateAsync(ActivityEntry dto);
        Task<bool> UpdateAsync(ActivityEntry dto);
        Task<bool> DeleteAsync(int id, string userId);
    }
}
