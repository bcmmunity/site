using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace site.Models
{
	public class Article
	{
		public int ArticleId { get; set; }
		public string Name { get; set; }
		public string PhotoCover { get; set; }
		public string Body { get; set; }
		public string Description { get; set; }
		public User Author { get; set; }
		public DateTime Date { get; set; }
		private string _time;

		public string Time
		{
			get
			{
				DateTime now = DateTime.Now;
				TimeSpan span = now - Date;
				if (span.Minutes < 1)
				{
					_time = $"{span.Seconds} с. назад";
				} else if (span.Hours < 1)
				{
					_time = $"{span.Minutes} мин. назад";
				
				} else if (span.Days < 1)
				{
					_time = $"{span.Hours} ч. назад";
				}
				else
				{
					_time = $"{span.Days} д назад";
				}

				return _time;
			}
		}

		[NotMapped]
		public List<Tag> Tags { get; set; }

		public Article()
		{
			Tags = new List<Tag>();
			
		}

	}
}
