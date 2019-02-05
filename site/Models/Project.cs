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
		[NotMapped]
		public List<Speciality> Specialities { get; set; }

		public Project()
		{
			Specialities = new List<Speciality>();
		}
	}
}
