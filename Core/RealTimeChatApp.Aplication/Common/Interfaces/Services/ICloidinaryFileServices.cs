using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;

namespace RealTimeChatApp.Aplication.Common.Interfaces.Services;

    public interface ICloidinaryFileServices
    {
        Task<ImageUploadResult> AddImageAsync(IFormFile file);
        Task<DeletionResult> DeleteImageAsync(string publicId);
    }
