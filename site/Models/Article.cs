using System;
using System.Collections.Generic;

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
		public List<Tag> Tags { get; set; }
	}
}
