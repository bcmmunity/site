using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace site.Models
{
	public class Project
	{
		public string Name { get; set; }
		public string Img { get; set; }
		public int ImgCount { get; set; }
		public string Description { get; set; }
		public Team Team { get; set; }
	}
}
