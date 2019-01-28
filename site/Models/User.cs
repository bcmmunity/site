﻿using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace site.Models
{
	public class User : IdentityUser
	{
		public string Photo { get; set; }
		public string Name { get; set; }
		public string Surname { get; set; }
		public string Position { get; set; }
		public string Description { get; set; }
		public List<Speciality> Specialities { get; set; }
	}
}
