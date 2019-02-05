using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace site.Models
{
	public class Team
	{
		public int TeamId { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		
		[NotMapped]
		public List<Project> Projects { get; set; }
		[NotMapped]
		public List<User> Members { get; set; }
		[NotMapped]
		public List<Speciality> Specialities { get; set; }

		public Team()
		{
			Projects = new List<Project>();
			Members = new List<User>();
			Specialities = new List<Speciality>();
		}
	}
}
