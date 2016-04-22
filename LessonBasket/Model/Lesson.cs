using System;
using System.Collections.Generic;

namespace LessonBasket
{
	public class Lesson
	{

		public int Id { get; set; }

		public string Description { get; set; }

		public IList<Screen> Screens { get; set; }

		public int screenCount { get; set; }

		public virtual string Title { get; set; }

		public string Url { get; set; }

		public Lesson ()
		{
		}
	}
}

