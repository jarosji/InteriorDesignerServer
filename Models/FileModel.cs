namespace InteriorDesigner.Models
{
    public class FileModel
    {  
        public FileModel()
        {
            //Project = new ProjectModel();
        }

        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string FilePath { get; set; } = string.Empty;


    }
}
