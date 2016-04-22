using System;
using System.Collections.Generic;

namespace LessonBasket
{
	public class Screen
	{
		public int Id { get; set; }

		public string Type { get; set; }

		public string Text { get; set; }

		public IList<Image> Images { get; set; }

		public string Question { get; set; }

		public string audio_url { get; set; }

		public string video_url { get; set; }

		public IList<Option> Options { get; set; }

		public int Position { get; set; }

		public Screen ()
		{
		}
	}
}

