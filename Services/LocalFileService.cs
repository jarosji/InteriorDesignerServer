using InteriorDesigner.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.IO;

namespace InteriorDesigner.Services
{
    public class LocalFileService : IFileService
    {

        private readonly IDContext dbContext;
        private readonly IWebHostEnvironment hostingEnvironment;

        public LocalFileService(IDContext dbContext, IWebHostEnvironment hostingEnvironment)
        {
            this.hostingEnvironment = hostingEnvironment;

            string rootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
            string uploadPath = Path.Combine(rootPath, "uploads");

            
            if (!Directory.Exists(rootPath))
                Directory.CreateDirectory(rootPath);

            if (!Directory.Exists(uploadPath))
                Directory.CreateDirectory(uploadPath);

            hostingEnvironment.WebRootPath = rootPath;
        }

        public FileStream? LoadFile(string userId, string fileName)
        {
            string filePath = Path.Combine(hostingEnvironment.WebRootPath, "uploads", userId, fileName);
            string filePathExt = Path.ChangeExtension(filePath, ".zip");

            if (System.IO.File.Exists(filePathExt))
            {
                return System.IO.File.OpenRead(filePathExt);
            }

            return null;
        }

        public async Task<string> SaveFileAsync(string userId, IFormFile file)
        {
            if (!System.IO.Directory.Exists(Path.Combine(hostingEnvironment.WebRootPath, "uploads", userId)))
                System.IO.Directory.CreateDirectory(Path.Combine(hostingEnvironment.WebRootPath, "uploads", userId));
            string filePath = Path.Combine(hostingEnvironment.WebRootPath, "uploads", userId, file.FileName);
            using (Stream fileStream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }

            return filePath;
        }
    }
}
