﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace site.Models
{
	public class Speciality
	{
		public int SpecialityId { get; set; }
		public string Name { get; set; }


		public override string ToString()
		{
			return Name;
		}
	}
}
