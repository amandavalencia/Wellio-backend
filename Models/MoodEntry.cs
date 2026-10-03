namespace Wellio.Models
{
    public class MoodEntry
    {
        public int Id { get; set; }

        public DateOnly Date { get; set; }

        public int Mood { get; set; }

        public int StressLevel { get; set; }

        public string? Note { get; set; }

        public string UserId { get; set; } = null!;
        public ApplicationUser User { get; set; } = null!;
    }
}