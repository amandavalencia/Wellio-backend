using System.ComponentModel.DataAnnotations;

namespace Wellio.DTO;

public class CreateMoodDto
{
    public DateOnly Date { get; set; }
    [Range(1, 5, ErrorMessage = "Mood måste vara mellan 1 och 5.")]

    public int Mood { get; set; }
    [Range(1, 5, ErrorMessage = "stresslevel måste vara mellan 1 och 5.")]

    public int StressLevel { get; set; }

    public string? Note { get; set; }
}

public class UpdateMoodDto
{
    public DateOnly Date { get; set; }

    public int Mood { get; set; }

    public int StressLevel { get; set; }

    public string? Note { get; set; }
}
public record MoodDto(int Id, DateOnly Date, int Mood, int StressLevel, string? Note);
