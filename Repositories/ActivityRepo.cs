using Microsoft.EntityFrameworkCore;
using Wellio.Data;
using Wellio.Models;
using Wellio.Repositories.IRepository;

namespace Wellio.Repositories
{
    public class ActivityRepo : IActivityRepo
    {
        private WellioDBContext context;

        public ActivityRepo(WellioDBContext _context)
        {
            context = _context;
        }

        public async Task<ActivityEntry> CreateAsync(ActivityEntry activity)
        {
            context.ActivityEntries.Add(activity);
            await context.SaveChangesAsync();
            return activity;
        }

        public async Task<bool> DeleteAsync(int id, string userId)
        {
            var rowsAffected = await context.ActivityEntries.
              Where(e => e.Id == id && e.UserId == userId).ExecuteDeleteAsync();

            if (rowsAffected > 0)
            {
                return true;
            }
            return false;
        }

        public async Task<List<ActivityEntry>> GetAllAsync(string userId)
        {
            return await context.ActivityEntries
                        .Where(entry => entry.UserId == userId)
                        .OrderByDescending(entry => entry.Date)
                        .ToListAsync();
        }

        public async Task<ActivityEntry?> GetByIdAsync(int activityId, string userId)
        {
            return await context.ActivityEntries
                        .Where(entry => entry.Id == activityId && entry.UserId == userId)
                        .FirstOrDefaultAsync();
        }

        public async Task<bool> UpdateAsync(ActivityEntry activity)
        {
            context.ActivityEntries.Update(activity);
            var result = await context.SaveChangesAsync();

            return true;
        }
    }
}
