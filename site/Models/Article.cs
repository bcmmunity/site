using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace site.Models
{
	public class Article
	{
		public int ArticleId { get; set; }
		public string Name { get; set; }
		public string Body { get; set; }
		public string Description { get; set; }
		public User Author { get; set; }
		public DateTime Date { get; set; }
		[NotMapped]
		public List<Tag> Tags { get; set; }

		public Article()
		{
			Tags = new List<Tag>();
		}

	}
}
