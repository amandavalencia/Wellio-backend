namespace Wellio.Models
{
    public class ActivityEntry
    {
        public int Id { get; set; }

        public DateOnly Date { get; set; }

        public string ActivityType { get; set; } = null!;

        public int DurationMinutes { get; set; }

        public int Intensity { get; set; }

        public string UserId { get; set; } = null!;
        public ApplicationUser User { get; set; } = null!;
    }
}