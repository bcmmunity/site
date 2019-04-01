using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace site.Models
{
	public class Project
	{
		public int ProjectId { get; set; }
		public string Name { get; set; }
		public string Img { get; set; }
		public string Description { get; set; }
		public int Rang { get; set; }
		public bool IsShowed { get; set; }
		[NotMapped]
		public User Leader { get; set; }
		[NotMapped]
		public List<string> SliderImages { get; set; }
		[NotMapped]
		public List<Speciality> Specialities { get; set; }
		
		public List<Link> Links { get; set; }

		
		public List<ProjectUser> Members { get; set; }
		

		public Project()
		{
			Specialities = new List<Speciality>();
			Links = new List<Link>();
			Members = new List<ProjectUser>();
			SliderImages = new List<string>();
		}
	}
}
