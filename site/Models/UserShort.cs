using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace site.Models
{
	public class UserShort
	{
		public string Email { get; set; }
		public string Name { get; set; }
		public string Surname { get; set; }
		public string Position { get; set; }
		public string PhoneNumber { get; set; }
		public string Photo { get; set; }
		public string Description { get; set; }
		public List<Speciality> Specialities { get; set; }
	}
}
