using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace site.Models
{
	public class Experience
	{
		public int ExperienceId { get; set; }
		public string Title { get; set; }
		public string Description { get; set; }
		public string Link { get; set; }
		public DateTime StartDate { get; set; }
		public DateTime FinishDate { get; set; }
	}
}
