using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace site.Models
{
	public class Team
	{
		public string Name { get; set; }
		public string Description { get; set; }
		public List<User> Members { get; set; }
	}
}
