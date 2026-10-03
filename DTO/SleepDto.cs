using System.ComponentModel.DataAnnotations;

namespace Wellio.DTO;

public class CreateSleepDto
{
    public DateOnly Date { get; set; }

    [Range(1, int.MaxValue)]
    public int DurationMinutes { get; set; }
    [Range(1, 5, ErrorMessage = "SleepQuality måste vara mellan 1 och 5.")]

    public int SleepQuality { get; set; }
}
public class UpdateSleepDto
{
    public DateOnly Date { get; set; }

    [Range(1, int.MaxValue)]
    public int DurationMinutes { get; set; }

    public int SleepQuality { get; set; }
}

public record SleepDto(int Id, DateOnly Date, int DurationMinutes, int SleepQuality);
