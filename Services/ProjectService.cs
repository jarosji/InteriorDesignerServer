using InteriorDesigner.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.IO;
using System.Security.Claims;

namespace InteriorDesigner.Services
{
    public class ProjectService : IProjectService
    {
        private readonly IDContext dbContext;
        private readonly IFileService fileService;

        public ProjectService(IDContext dbContext, IFileService fileService)
        {
            this.dbContext = dbContext;
            this.fileService = fileService;
        }

        public async Task<ProjectModel?> CreateProjectAsync(UserModel user, IFormFile file)
        {
            if (file.Length > 0)
            {
                string filePath = await fileService.SaveFileAsync(user.UserName, file);

                FileModel NewFile = new FileModel()
                {
                    Name = file.FileName,
                    FilePath = filePath
                };
                await dbContext.Files.AddAsync(NewFile);

                ProjectModel NewProject = new ProjectModel()
                {
                    File = NewFile,
                    Name = Path.GetFileNameWithoutExtension(file.FileName),
                    User = user,
                };
                await dbContext.Projects.AddAsync(NewProject);
                await dbContext.SaveChangesAsync();

                return NewProject;
            }

            return null;
        }

        public async Task<ProjectModel> GetProjectAsync(int projectId)
        {
            return await dbContext.Projects.FirstAsync(x=>x.Id == projectId);
        }

        public async Task<ProjectModel?> GetProjectAsync(string projectName)
        {
            return await dbContext.Projects.FirstOrDefaultAsync(x => x.Name == projectName);
        }

        public async Task<ProjectModel?> GetProjectByUserAsync(string userId, string projectName)
        {
            return await dbContext.Projects
                .Where(x => x.User.Id == userId)
                .Where(x => x.Name == projectName)
                .FirstOrDefaultAsync();
        }

        public async Task<ICollection<ProjectModel>> GetProjectsByUserAsync(string userId)
        {
            return await dbContext.Projects
                .Where(x => x.User.Id == userId)
                .Select(x => new ProjectModel() { Name = x.Name, File = x.File })
                .ToListAsync();
        }
    }
}
