using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Wellio.Models;

namespace Wellio.Data
{
    public class WellioDBContext : IdentityDbContext<ApplicationUser>
    {
        public WellioDBContext(DbContextOptions<WellioDBContext> options) : base(options)
        {
        }

        public DbSet<MoodEntry> MoodEntries { get; set; } = null!;
        public DbSet<SleepEntry> SleepEntries { get; set; } = null!;
        public DbSet<ActivityEntry> ActivityEntries { get; set; } = null!;
    }
}
