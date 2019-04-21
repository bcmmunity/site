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
		public string Status { get; set; }
		public string Type { get; set; }
		public int Rang { get; set; }
		public bool IsShowed { get; set; }
		// [NotMapped]
		// public User Leader { get; set; }
		
		public string SliderImages { get; set; }

		
		public List<Link> Links { get; set; }

		
		public List<ProjectUser> Members { get; set; }
		public List<ProjectSpec> Specialities { get; set; }

		public Project()
		{
			Specialities = new List<ProjectSpec>();
			Links = new List<Link>();
			Members = new List<ProjectUser>();
		}
	}
}
