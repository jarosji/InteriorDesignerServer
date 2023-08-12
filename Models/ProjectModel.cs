namespace InteriorDesigner.Models
{
    public class ProjectModel
    {
        public ProjectModel()
        {
            File = new FileModel();
            User = new UserModel();
        }

        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;

        public virtual FileModel File { get; set; }

        public virtual UserModel User { get; set; }
    }
}
