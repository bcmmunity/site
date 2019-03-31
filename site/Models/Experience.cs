using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace site.Models
{
	public class Experience
	{
		public int ExperienceId { get; set; }
		public string Title { get; set; }
		public string Description { get; set; }
		public string Link { get; set; }
		public DateTime StartDate { get; set; }
		public DateTime FinishDate { get; set; }
		public bool IsWork { get; set; }
		private string _year;
		public string Year
		{
			get
			{
				string tempMonthStart = StartDate.Month.ToString().Length == 1 ? $"0{StartDate.Month}" : StartDate.Month.ToString();
				string tempMonthFinish = FinishDate.Month.ToString().Length == 1 ? $"0{FinishDate.Month}" : FinishDate.Month.ToString();
				_year = $"{tempMonthStart}.{StartDate.Year}-{tempMonthFinish}.{FinishDate.Year}";
				return _year;
			}
		}
	}
}
