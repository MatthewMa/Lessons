using System;
using System.Collections.Generic;
using Org.Json;

namespace LessonBasketDemo
{
	public class Screen
	{
		public string question{ get; set; }

		public string text { get; set; }

		public int id{ get; set; }

		public string type{ get; set; }

		public List<String> choices{ get; set; }
		//TODO add the choice object with the detail
		public string videoUrl { get; set; }

		public string audioUrl{ get; set; }

		public List<ScreenImage> screenImages { get; set; }

		public Screen ()
		{
			
		}

		public Screen (JSONObject jsonScreen)
		{
			choices = new List<String> ();
			screenImages = new List<ScreenImage> ();
			JSONArray jsonChoices;
			JSONArray jsonImages;

			id = jsonScreen.GetInt ("id");
			type = jsonScreen.GetString ("type");

			switch (type) {

			case "video":
				videoUrl = jsonScreen.GetString ("video_url");
				break;

			case "audio_question":
				audioUrl = jsonScreen.GetString ("audio_url");
				question = jsonScreen.GetString ("question");
				jsonChoices = jsonScreen.GetJSONArray ("choices");
				for (int i = 0; i < jsonChoices.Length (); i++) {
					choices.Add (jsonChoices.GetJSONObject (i).GetString ("title"));
				}

				break;

			case "audio_question_image":
				audioUrl = jsonScreen.GetString ("audio_url");
				question = jsonScreen.GetString ("question");
				jsonChoices = jsonScreen.GetJSONArray ("choices");
				jsonImages = jsonScreen.GetJSONArray ("images");
				for (int i = 0; i < jsonChoices.Length (); i++) {
					choices.Add (jsonChoices.GetJSONObject (i).GetString ("title"));
				}
				for (int i = 0; i < jsonImages.Length (); i++) {
					screenImages.Add (new ScreenImage (jsonImages.GetJSONObject (i).GetString ("url"), jsonImages.GetJSONObject (i).GetString ("title")));
				}
				break;

			case "audio_text":
				audioUrl = jsonScreen.GetString ("audio_url");
				text = jsonScreen.GetString ("text");

				break;
			
			case "audio_edittext":
				audioUrl = jsonScreen.GetString ("audio_url");
				text = jsonScreen.GetString ("text");
				jsonImages = jsonScreen.GetJSONArray ("images");
				for (int i = 0; i < jsonImages.Length (); i++) {
					screenImages.Add (new ScreenImage (jsonImages.GetJSONObject (i).GetString ("url"), jsonImages.GetJSONObject (i).GetString ("title")));
				}

				break;

			default:
				break;
				

			}
		}
	}


}

