using InteriorDesigner.Models;

namespace InteriorDesigner.Services
{
    public interface IProjectService
    {
        Task<ProjectModel?> GetProjectByUserAsync(string userId, string projectName);
        Task<ICollection<ProjectModel>> GetProjectsByUserAsync(string userId);

        Task<ProjectModel> GetProjectAsync(int projectId);
        Task<ProjectModel?> GetProjectAsync(string projectName);

        Task<ProjectModel?> CreateProjectAsync(UserModel user, IFormFile file);
    }
}
