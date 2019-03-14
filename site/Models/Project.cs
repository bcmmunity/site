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
		public User Leader { get; set; }
		[NotMapped]
		public List<string> SliderImages { get; set; }
		[NotMapped]
		public List<Speciality> Specialities { get; set; }
		[NotMapped]
		public List<Link> Links { get; set; }
		[NotMapped]
		public List<Social> Socials { get; set; }
		[NotMapped]
		public Team Team { get; set; }

		public Project()
		{
			Specialities = new List<Speciality>();
			Links = new List<Link>();
			Socials = new List<Social>();
			SliderImages = new List<string>();
		}
	}
}
