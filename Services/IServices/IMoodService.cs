using Wellio.DTO;

namespace Wellio.Services.IServices
{
    public interface IMoodService
    {
        Task<List<MoodDto>> GetAllAsync(string userId);
        Task<MoodDto?> GetByIdAsync(int id, string userId);
        Task<MoodDto> CreateAsync(CreateMoodDto dto, string userId);
        Task<bool> UpdateAsync(int id, UpdateMoodDto dto, string userId);
        Task<bool> DeleteAsync(int id, string userId);
    }
}
