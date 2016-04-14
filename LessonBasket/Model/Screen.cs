using System;
using System.Collections.Generic;

namespace LessonBasket
{
	public class Screen
	{
		public int id { get; set;}
		public string type { get; set;}
		public string text { get; set;}
		public IList<String> images { get; set;}
		public string question { get; set;}
		public string audio_url {get; set;}
		public string video_url { get; set;}
		public IList<String> questions { get; set;} //TODO change to options when the json is changed


		public Screen ()
		{
		}
	}
}

