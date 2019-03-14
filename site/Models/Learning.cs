using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace site.Models
{
	public class Learning
	{
		public int LearningId { get; set; }
		public string Title { get; set; }
		public string Description { get; set; }
		public string Link { get; set; }
		public DateTime StartDate { get; set; }
		public DateTime FinishDate { get; set; }
	}
}
