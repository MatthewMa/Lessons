using System;
using System.Collections.Generic;

namespace LessonBasketDemo
{
	public static class Constants
	{
		public static readonly string server = "http://mgl.usask.ca:8080/usis/rest/lessons/1";
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
		public static List<Lecture> lists;
	}
}

