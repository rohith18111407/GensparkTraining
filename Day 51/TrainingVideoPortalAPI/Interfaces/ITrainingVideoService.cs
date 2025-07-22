using TrainingVideoPortalAPI.Models;
using TrainingVideoPortalAPI.Models.DTOs;

namespace TrainingVideoPortalAPI.Services
{
    public interface ITrainingVideoService
    {
        Task<TrainingVideoResponseDto> UploadVideoAsync(TrainingVideoUploadRequestDto dto);
        Task<IEnumerable<TrainingVideoResponseDto>> GetAllVideosAsync();
        Task<TrainingVideoResponseDto?> GetVideoByIdAsync(Guid id);
    }

}