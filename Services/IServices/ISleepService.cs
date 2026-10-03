using Wellio.DTO;

namespace Wellio.Services.IServices
{
    public interface ISleepService
    {
        Task<List<SleepDto>> GetAllAsync(string userId);

        Task<SleepDto?> GetByIdAsync(int id, string userId);

        Task<SleepDto> CreateAsync(CreateSleepDto dto, string userId);

        Task<bool> UpdateAsync(int id, UpdateSleepDto dto, string userId);

        Task<bool> DeleteAsync(int id, string userId);
    }
}
