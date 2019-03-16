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
		
		private string _year;
		public string Year
		{
			get
			{
				if (FinishDate.Year == StartDate.Year)
					_year = FinishDate.Year.ToString();
				else
					_year = $"{StartDate.Year}-{FinishDate.Year}";
				return _year;
			}
		}
	}
}
