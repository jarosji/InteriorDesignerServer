using Microsoft.AspNetCore.Identity;
using System.Data;

namespace InteriorDesigner.Models
{
    public class UserModel : IdentityUser
    {
        public UserModel()
        {
            Projects = new List<ProjectModel>();
        }

        public virtual ICollection<ProjectModel> Projects { get; set; }
    }
}
