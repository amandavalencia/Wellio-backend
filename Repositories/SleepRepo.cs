using Microsoft.EntityFrameworkCore;
using Wellio.Data;
using Wellio.Models;
using Wellio.Repositories.IRepository;

namespace Wellio.Repositories
{
    public class SleepRepo : ISleepRepo
    {
        private WellioDBContext context;

        public SleepRepo(WellioDBContext _context)
        {
            context = _context;
        }

        public async Task<SleepEntry> CreateAsync(SleepEntry sleep)
        {
            context.SleepEntries.Add(sleep);
            await context.SaveChangesAsync();
            return sleep;
        }

        public async Task<bool> DeleteAsync(int id, string userId)
        {
            var rowsAffected = await context.SleepEntries.
              Where(e => e.Id == id && e.UserId == userId).ExecuteDeleteAsync();

            if (rowsAffected > 0)
            {
                return true;
            }
            return false;
        }

        public async Task<List<SleepEntry>> GetAllAsync(string userId)
        {
            return await context.SleepEntries
                        .Where(entry => entry.UserId == userId)
                        .OrderByDescending(entry => entry.Date)
                        .ToListAsync();
        }

        public async Task<SleepEntry?> GetByIdAsync(int sleepId, string userId)
        {
            return await context.SleepEntries
                        .Where(entry => entry.Id == sleepId && entry.UserId == userId)
                        .FirstOrDefaultAsync();
        }

        public async Task<bool> UpdateAsync(SleepEntry sleep)
        {
            context.SleepEntries.Update(sleep);
            await context.SaveChangesAsync();

            return true;
        }
    }
}
