using Wellio.DTO;
using Wellio.Models;
using Wellio.Repositories.IRepository;
using Wellio.Services.IServices;

namespace Wellio.Services;

public class MoodService : IMoodService
{
    private readonly IMoodRepo _repository;

    public MoodService(IMoodRepo repository)
    {
        _repository = repository;
    }
    public async Task<List<MoodDto>> GetAllAsync(string userId)
    {
        var allMoods = await _repository.GetAllAsync(userId);

        return allMoods.Select(mood => new MoodDto(
             mood.Id,
             mood.Date,
             mood.Mood,
             mood.StressLevel,
             mood.Note
        )).ToList();
    }

    public async Task<MoodDto?> GetByIdAsync(int id, string userId)
    {

        var entry = await _repository.GetByIdAsync(id, userId);

        if (entry == null)
        {
            return null;
        }

        return ToResponse(entry);
    }

    public async Task<MoodDto> CreateAsync(CreateMoodDto newMoodDto, string userId)
    {
        var mood = new MoodEntry
        {
            UserId = userId,
            Date = newMoodDto.Date,
            Mood = newMoodDto.Mood,
            StressLevel = newMoodDto.StressLevel,
            Note = newMoodDto.Note,
        };

        await _repository.CreateAsync(mood);
        return ToResponse(mood);
    }

    public async Task<bool> UpdateAsync(int id, UpdateMoodDto newMoodDto, string userId)
    {
        var mood = await _repository.GetByIdAsync(id, userId);

        if (mood == null)
        {
            return false;
        }

        mood.Date = newMoodDto.Date;
        mood.Mood = newMoodDto.Mood;
        mood.StressLevel = newMoodDto.StressLevel;
        mood.Note = newMoodDto.Note;
        return await _repository.UpdateAsync(mood);

    }

    public async Task<bool> DeleteAsync(int id, string userId)
    {

        return await _repository.DeleteAsync(id, userId);

    }

    private static MoodDto ToResponse(MoodEntry entry)
    {
        return new MoodDto(entry.Id, entry.Date, entry.Mood, entry.StressLevel, entry.Note);
    }
}
