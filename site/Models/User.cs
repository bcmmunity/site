using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace site.Models
{
	public class User : IdentityUser
	{
		public string Photo { get; set; }
		public List<Speciality> Specialities { get; set; }
	}
}
