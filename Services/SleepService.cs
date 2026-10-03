using Wellio.DTO;
using Wellio.Models;
using Wellio.Repositories.IRepository;
using Wellio.Services.IServices;

namespace Wellio.Services;

public class SleepService : ISleepService
{
    private readonly ISleepRepo _repository;

    public SleepService(ISleepRepo repository)
    {
        _repository = repository;
    }
    public async Task<List<SleepDto>> GetAllAsync(string userId)
    {
        var allSleeps = await _repository.GetAllAsync(userId);

        return allSleeps.Select(sleep => new SleepDto(
             sleep.Id,
             sleep.Date,
             sleep.DurationMinutes,
             sleep.SleepQuality
        )).ToList();
    }

    public async Task<SleepDto?> GetByIdAsync(int id, string userId)
    {

        var entry = await _repository.GetByIdAsync(id, userId);

        if (entry == null)
        {
            return null;
        }

        return ToResponse(entry);
    }

    public async Task<SleepDto> CreateAsync(CreateSleepDto newSleepDto, string userId)
    {
        var sleep = new SleepEntry
        {
            UserId = userId,
            Date = newSleepDto.Date,
            DurationMinutes = newSleepDto.DurationMinutes,
            SleepQuality = newSleepDto.SleepQuality,
        };

        await _repository.CreateAsync(sleep);
        return ToResponse(sleep);
    }

    public async Task<bool> UpdateAsync(int id, UpdateSleepDto newSleepDto, string userId)
    {
        var sleep = await _repository.GetByIdAsync(id, userId);

        if (sleep == null)
        {
            return false;
        }

        sleep.Date = newSleepDto.Date;
        sleep.DurationMinutes = newSleepDto.DurationMinutes;
        sleep.SleepQuality = newSleepDto.SleepQuality;
        return await _repository.UpdateAsync(sleep);

    }

    public async Task<bool> DeleteAsync(int id, string userId)
    {

        return await _repository.DeleteAsync(id, userId);

    }

    private static SleepDto ToResponse(SleepEntry entry)
    {
        return new SleepDto(entry.Id, entry.Date, entry.DurationMinutes, entry.SleepQuality);
    }
}
