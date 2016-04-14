using System;
using System.Collections.Generic;
using Org.Json;

namespace LessonBasketDemo
{
	public class Lecture
	{
		public string title { get; set; }

		public List<Screen> screens{ get; set; }

		public int id{ get; set; }

		public int screenCount { get; set; }

		public string description { get; set; }


		public Lecture ()
		{
		}

		public Lecture (JSONObject lectureJson)
		{
			//Start pasging the JSON into a lecture

			title = lectureJson.GetString ("title");
			id = lectureJson.GetInt ("id");
			screenCount = lectureJson.GetInt ("screenCount");
			description = lectureJson.GetString ("description");
			screens = new List<Screen> ();
			JSONArray jsonScreens = lectureJson.GetJSONArray ("screens");
			for (int i = 0; i < jsonScreens.Length (); i++) {
				screens.Add (new Screen (jsonScreens.GetJSONObject (i)));
			}
				
			/*for(int i=0 ; i < questionsJson.Length() ; i++){
				listQuestions.Add (new Question(questionsJson.GetJSONObject(i)));
			}
			return listQuestions;*/
		}
	}
}

