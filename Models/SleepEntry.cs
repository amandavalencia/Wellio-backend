using Microsoft.EntityFrameworkCore;

namespace Wellio.Models
{
    [Index(nameof(UserId), nameof(Date))]
    public class SleepEntry
    {
        public int Id { get; set; }

        public DateOnly Date { get; set; }

        public int DurationMinutes { get; set; }

        public int SleepQuality { get; set; }

        public string UserId { get; set; } = null!;
        public ApplicationUser User { get; set; } = null!;
    }
}