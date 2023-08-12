using InteriorDesigner.Models;
using InteriorDesigner.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting.Internal;
using System.Security.Claims;

namespace InteriorDesigner.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class FileController : Controller
    {
        private readonly IProjectService projectService;
        private readonly IFileService fileService;
        private readonly IUserService userService;

        public FileController(IUserService userService, IProjectService projectService, IFileService fileService)
        {
            this.projectService = projectService;
            this.fileService = fileService;
            this.userService = userService;
        }

        [HttpGet]
        [Route("MyProjects")]
        public async Task<IActionResult> GetUserProjects()
        {
            string userName = User.FindFirstValue(ClaimTypes.Name);
            var user = await userService.GetUserByUsernameAsync(userName);

            if(user != null)
            {
                var Projects = await projectService.GetProjectsByUserAsync(user.Id);

                return Json(new { Projects = Projects });
            }

            return NotFound();
        }

        [HttpPost]
        [Route("upload_file")]
        public async Task<IActionResult> UploadFile(IFormFile file)
        {
            string userId = User.FindFirstValue(ClaimTypes.Name);
            UserModel? user = await userService.GetUserByUsernameAsync(userId);
            if(user == null)
                return BadRequest();

            ProjectModel? FoundProject = await projectService.GetProjectByUserAsync(user.Id, Path.GetFileNameWithoutExtension(file.FileName));//user.Projects.Where(x=>x.Name == file.Name).FirstOrDefault();
            if (FoundProject != null)
            {
                await fileService.SaveFileAsync(user.UserName, file);
                return Ok(new { FoundProject.Name, FoundProject.File });
            }

            ProjectModel? NewProject = await projectService.CreateProjectAsync(user, file);
            if (NewProject == null)
                return BadRequest();

            return Ok(new { NewProject.Name, NewProject.File});
        }

        [HttpGet]
        [Route("download_file")]
        public IActionResult DownloadFile(string name)
        {
            string userId = User.FindFirstValue(ClaimTypes.Name);
            FileStream? file = fileService.LoadFile(userId, name);
            if(file != null)
            {
                return File(file, "application/octet-stream", name);
            }

            return NotFound();
        }
    }
}
