using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace site.Models
{
	public class User : IdentityUser
	{
		//[Key]
		//[DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
		public string Photo { get; set; }
		public string Name { get; set; }
		public string Surname { get; set; }
		public string Position { get; set; }
		public string Description { get; set; }
		public int Rang { get; set; }
		public bool IsShowed { get; set; }
		public List<UserSpec> Specialities { get; set; }
		public List<Project> LeaderProjects { get; set; }
		
		public List<ProjectUser> Projects{ get; set; }
		
		public List<Link> Links { get; set; }
		public List<Experience> Experiences { get; set; }

		public User()
		{
			Experiences = new List<Experience>();
			Links = new List<Link>();
			LeaderProjects = new List<Project>();
			Specialities = new List<UserSpec>();
			Projects = new List<ProjectUser>();
		}
	}
}
