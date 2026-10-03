using System.ComponentModel.DataAnnotations;

namespace Wellio.DTO;

public class CreateActivityDto
{
    public DateOnly Date { get; set; }

    [Required]
    public string ActivityType { get; set; } = string.Empty;

    [Range(1, int.MaxValue)]
    public int DurationMinutes { get; set; }

    [Range(1, 5, ErrorMessage = "Intensiteten måste vara mellan 1 och 5.")]
    public int Intensity { get; set; }
}
public class UpdateActivityDto
{
    public DateOnly Date { get; set; }

    [Required]
    public string ActivityType { get; set; } = string.Empty;

    [Range(1, int.MaxValue)]
    public int DurationMinutes { get; set; }

    public int Intensity { get; set; }
}

public record ActivityDto(int Id, DateOnly Date, string ActivityType, int DurationMinutes, int Intensity);
