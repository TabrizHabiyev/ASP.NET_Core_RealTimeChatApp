

namespace RealTimeChatApp.Aplication.Common.Interfaces.Services;

    public interface ICloidinaryFileServices
    {
        Task<ImageUploadResult> AddImageAsync(IFormFile file);
        Task<DeletionResult> DeleteImageAsync(string publicId);
    }
