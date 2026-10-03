using Microsoft.AspNetCore.Identity;
namespace Wellio.Models
{
    public class ApplicationUser : IdentityUser
    {
        public ICollection<MoodEntry> MoodEntries { get; set; } = [];
        public ICollection<SleepEntry> SleepEntries { get; set; } = [];
        public ICollection<ActivityEntry> ActivityEntries { get; set; } = [];
    }
}
