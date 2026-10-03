using Wellio.Models;

namespace Wellio.Repositories.IRepository
{
    public interface ISleepRepo
    {
        Task<List<SleepEntry>> GetAllAsync(string userId);
        Task<SleepEntry?> GetByIdAsync(int id, string userId);
        Task<SleepEntry> CreateAsync(SleepEntry dto);
        Task<bool> UpdateAsync(SleepEntry dto);
        Task<bool> DeleteAsync(int id, string userId);
    }
}
