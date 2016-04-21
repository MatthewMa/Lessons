using System;
using System.Collections.Generic;
using LessonBasket;

namespace LessonBasketDemo
{
	public static class Constants
	{
		public static readonly long secondMillis = 1000;
		public static readonly long minuteMillis = 60 * secondMillis;
		public static readonly long hourMillis = 60 * minuteMillis;
		/** update system time */
		public static readonly int UPDATE_SYSTEM_TIME = 0;
		/** update current play-time */
		public static readonly int UPDATE_CURRENT_POSITION = 1;
		/** hide control layout */
		public static readonly int HIDE_CTRL_LAYOUT = 2;

		//Error status
		public const int REQUEST_FAILED = 0;
		public const int NET_ERROR = 1;
		public static List<Lesson> lessons_url;
		public static readonly string VIDEO_URL = "https://www.lessonbasket.com/desktopmodules/lessonbasket/projects/199/241/155.brave%20archery%20scene.mp4";
	}
}

