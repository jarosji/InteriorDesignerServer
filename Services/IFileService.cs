using InteriorDesigner.Models;

namespace InteriorDesigner.Services
{
    public interface IFileService
    {
        Task<string> SaveFileAsync(string userId, IFormFile file);
        FileStream? LoadFile(string uiserId, string fileName);
    }
}
