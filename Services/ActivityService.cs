using Wellio.DTO;
using Wellio.Models;
using Wellio.Repositories.IRepository;
using Wellio.Services.IServices;

namespace Wellio.Services;

public class ActivityService : IActivityService
{
    private readonly IActivityRepo _repository;

    public ActivityService(IActivityRepo repository)
    {
        _repository = repository;
    }
    //service ska returnera användares aktivitet som dto och tar emot aktiviteten som en activityentry.
    // Filtrera på användaren så att varje person bara kommer åt sina egna poster.
    public async Task<List<ActivityDto>> GetAllAsync(string userId)
    {
        var allActivities = await _repository.GetAllAsync(userId);

        return allActivities.Select(activity => new ActivityDto(
             activity.Id,
             activity.Date,
             activity.ActivityType,
             activity.DurationMinutes,
             activity.Intensity
        )).ToList();
    }

    public async Task<ActivityDto?> GetByIdAsync(int id, string userId)
    {

        var entry = await _repository.GetByIdAsync(id, userId);

        if (entry == null)
        {
            return null;
        }

        return ToResponse(entry);
    }

    public async Task<ActivityDto> CreateAsync(CreateActivityDto newActivityDto, string userId)
    {
        var activity = new ActivityEntry
        {
            UserId = userId,
            Date = newActivityDto.Date,
            ActivityType = newActivityDto.ActivityType,
            DurationMinutes = newActivityDto.DurationMinutes,
            Intensity = newActivityDto.Intensity,
        };

        await _repository.CreateAsync(activity);
        return ToResponse(activity);
    }

    public async Task<bool> UpdateAsync(int id, UpdateActivityDto newActivityDto, string userId)
    {
        var activity = await _repository.GetByIdAsync(id, userId);

        if (activity == null)
        {
            return false;
        }

        activity.Date = newActivityDto.Date;
        activity.ActivityType = newActivityDto.ActivityType;
        activity.DurationMinutes = newActivityDto.DurationMinutes;
        activity.Intensity = newActivityDto.Intensity;
        return await _repository.UpdateAsync(activity);

    }

    public async Task<bool> DeleteAsync(int id, string userId)
    {

        return await _repository.DeleteAsync(id, userId);

    }

    private static ActivityDto ToResponse(ActivityEntry entry)
    {
        return new ActivityDto(entry.Id, entry.Date, entry.ActivityType, entry.DurationMinutes, entry.Intensity);
    }
}
