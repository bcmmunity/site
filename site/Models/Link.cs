using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace site.Models
{
	public class Link
	{
		public int LinkId { get; set; }
		public string Href { get; set; }
		public string Title { get; set; }
		public string Pic { get; set; }
		public bool IsSocial { get; set; }
	}
}
