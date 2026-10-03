using Microsoft.EntityFrameworkCore;
using Wellio.Data;
using Wellio.Models;
using Wellio.Repositories.IRepository;

namespace Wellio.Repositories
{
    public class MoodRepo : IMoodRepo
    {
        private WellioDBContext context;

        public MoodRepo(WellioDBContext _context)
        {
            context = _context;
        }

        public async Task<MoodEntry> CreateAsync(MoodEntry mood)
        {
            context.MoodEntries.Add(mood);
            await context.SaveChangesAsync();
            return mood;
        }

        public async Task<bool> DeleteAsync(int id, string userId)
        {
            var rowsAffected = await context.MoodEntries.
              Where(e => e.Id == id && e.UserId == userId).ExecuteDeleteAsync();

            if (rowsAffected > 0)
            {
                return true;
            }
            return false;
        }

        public async Task<List<MoodEntry>> GetAllAsync(string userId)
        {
            return await context.MoodEntries
                        .Where(entry => entry.UserId == userId)
                        .OrderByDescending(entry => entry.Date)
                        .ToListAsync();
        }

        public async Task<MoodEntry?> GetByIdAsync(int moodId, string userId)
        {
            return await context.MoodEntries
                        .Where(entry => entry.Id == moodId && entry.UserId == userId)
                        .FirstOrDefaultAsync();
        }

        public async Task<bool> UpdateAsync(MoodEntry mood)
        {
            context.MoodEntries.Update(mood);
            await context.SaveChangesAsync();

            return true;
        }
    }
}
