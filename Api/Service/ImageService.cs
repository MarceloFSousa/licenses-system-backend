using Microsoft.AspNetCore.Http;

namespace Api.Service
{
    public interface IImageService
    {
        Task<byte[]> ConvertToBytesAsync(IFormFile file);
        Task<string> GetExtension(IFormFile file);
    }

    public class ImageService : IImageService
    {
        public async Task<byte[]> ConvertToBytesAsync(IFormFile file)
        {
            if (file == null || file.Length == 0)
                throw new ArgumentException("File is empty or missing.", nameof(file));

            using var ms = new MemoryStream();
            await file.CopyToAsync(ms);
            return ms.ToArray();


        }
        public async Task<string> GetExtension(IFormFile file)
        {
            // Get extension from content type
            var extension = Path.GetExtension(file.FileName);
            if (string.IsNullOrEmpty(extension))
            {
                extension = file.ContentType switch
                {
                    "image/jpeg" => ".jpeg",
                    "image/png" => ".png",
                    "image/gif" => ".gif",
                    _ => ".bin"
                };
            }
            return extension;

        }
    }
}
