using Wellio.DTO;

namespace Wellio.Services.IServices
{
    public interface IActivityService
    {
        Task<List<ActivityDto>> GetAllAsync(string userId);
        Task<ActivityDto?> GetByIdAsync(int id, string userId);
        Task<ActivityDto> CreateAsync(CreateActivityDto dto, string userId);
        Task<bool> UpdateAsync(int id, UpdateActivityDto dto, string userId);
        Task<bool> DeleteAsync(int id, string userId);
    }
}
