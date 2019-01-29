using System.Collections.Generic;

namespace site.Models
{
	public class Project
	{
		public int ProjectId { get; set; }
		public string Name { get; set; }
		public string Img { get; set; }
		public string Description { get; set; }
		public List<Speciality> Specialities { get; set; }
	}
}
