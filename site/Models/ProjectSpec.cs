namespace site.Models
{
    public class ProjectSpec
    {
        public int SpecialityId { get; set; }
        public Speciality Speciality { get; set; }
        
        public int ProjectId { get; set; }
        public Project Project { get; set; }
    }
}