using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace site.Models
{
	public class Social
	{
		public int SocialId { get; set; }
		public string Href { get; set; }
//		[NotMapped]
//		public SN Type { get; set; }
		public string Title { get; set; }
		public string Pic { get; set; }
	}
}
