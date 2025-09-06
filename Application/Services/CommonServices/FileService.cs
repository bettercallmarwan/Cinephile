using Application.Exceptions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace Application.Services.CommonServices
{
    public class FileService
    {
        private readonly IHostingEnvironment _env;

        public FileService(IHostingEnvironment env)
        {
            _env = env;
        }

        public async Task<string> Upload(IFormFile file, string folderName)
        {
            if (file is null || file.Length == 0)
                throw new BadRequestException("No file uploaded");

            var webRoot = _env.WebRootPath ?? Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
            var uploadsRoot = Path.Combine(webRoot, folderName);

            Directory.CreateDirectory(uploadsRoot);

            var fileName = file.FileName;

            var fullPath = Path.Combine(uploadsRoot, fileName);

            await using (var stream = System.IO.File.Create(fullPath))
            {
                await file.CopyToAsync(stream);
            }

            return $"{folderName}/{fileName}";
        }
    }
}
