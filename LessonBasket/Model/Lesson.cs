﻿using System;
using System.Collections.Generic;

namespace LessonBasket
{
	public class Lesson
	{

		public int id { get; set;}
		public string description { get; set;}
		public IList<string> screenList { get; set;} //TODO this should not be string but the Screen object
		public int screenCount { get; set;}
		public string title { get; set;}

		public Lesson ()
		{
		}
	}
}

