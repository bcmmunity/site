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
		public List<User> Members { get; set; }

		public Team()
		{
			Members = new List<User>();
		}
	}
}
