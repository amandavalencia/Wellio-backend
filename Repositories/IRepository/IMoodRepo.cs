using Wellio.Models;

namespace Wellio.Repositories.IRepository
{
    public interface IMoodRepo
    {
        Task<List<MoodEntry>> GetAllAsync(string userId);
        Task<MoodEntry?> GetByIdAsync(int id, string userId);
        Task<MoodEntry> CreateAsync(MoodEntry dto);
        Task<bool> UpdateAsync(MoodEntry dto);
        Task<bool> DeleteAsync(int id, string userId);
    }
}
