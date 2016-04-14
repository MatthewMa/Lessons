using System;

namespace LessonBasketDemo
{
	public class ScreenImage
	{

		public string imageUrl { get; set; }

		public string titleUrl { get; set; }

		public ScreenImage ()
		{
		}

		public ScreenImage (String imageUrl, String titleUrl)
		{
			this.imageUrl = imageUrl;
			this.titleUrl = titleUrl;
		}
	}
}

