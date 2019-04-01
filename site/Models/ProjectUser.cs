namespace site.Models
{
    public class ProjectUser
    {
        public int ProjectId { get; set; }
        public Project Project { get; set; }
        
        public string Id { get; set; }
        public User User { get; set; }
    }
}