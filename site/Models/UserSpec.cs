namespace site.Models
{
    public class UserSpec
    {
        public int SpecialityId { get; set; }
        public Speciality Speciality { get; set; }
        
        public string Id { get; set; }
        public User User { get; set; }
    }
}