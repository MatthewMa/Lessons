using System;
using System.Net;
using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using System.Json;

namespace LessonBasket
{
	public static class LessonUtil
	{
		private static string LESSONS_URL="http://mgl.usask.ca:8080/usis/rest/lessons/";



		/// <summary>Get the lesson list.
		/// <para>Returns a list of string pointing to each lesson</para>
		/// </summary>
		public static async Task<IList<String>> getLessonListFromRest ()
		{
			IList<String> lessonList;
			HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create (new Uri (LESSONS_URL)); 
			request.ContentType = "application/json";
			request.Method = "GET";

			using (WebResponse response = await request.GetResponseAsync ())
			{
				using (Stream stream = response.GetResponseStream ())
				{
					JsonValue jsonDoc = await Task.Run (() => JsonObject.Load (stream));
					JArray lessonArray = JArray.Parse(jsonDoc.ToString());
					lessonList = JsonConvert.DeserializeObject<IList<String>>(lessonArray.ToString());
				}
			}
			return lessonList;
		}


		/// <summary>Get a specific image.
		/// <para>Returns a image object from the image Rest urle</para>
		/// </summary>
		public static async Task<Image> getImageFromRest (string imageUrl)
		{
			Image image;
			HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create (new Uri (imageUrl)); 
			request.ContentType = "application/json";
			request.Method = "GET";

			using (WebResponse response = await request.GetResponseAsync ())
			{
				using (Stream stream = response.GetResponseStream ())
				{
					JsonValue jsonDoc = await Task.Run (() => JsonObject.Load (stream));
					image = JsonConvert.DeserializeObject<Image>(jsonDoc.ToString());
				}
			}
			return image;
		}

		/// <summary>Get a specific screen.
		/// <para>Returns a screen object from the screen Rest url</para>
		/// </summary>
		public static async Task<Screen> getLessonScreenFromRest (string screenUrl){

			Screen screen;
			HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create (new Uri (screenUrl)); 
			request.ContentType = "application/json";
			request.Method = "GET";

			using (WebResponse response = await request.GetResponseAsync ())
			{
				// Get a stream representation of the HTTP web response:
				using (Stream stream = response.GetResponseStream ())
				{
					JsonValue jsonDoc = await Task.Run (() => JsonObject.Load (stream));
					screen = JsonConvert.DeserializeObject<Screen>(jsonDoc.ToString());
				}
			}
			return screen;
		}

		/// <summary>Get a specific lesson.
		/// <para>Returns a lesson object from the lesson Rest url</para>
		/// </summary>
		public static async Task<Lesson> getLessonFromRest (string lessonUrl)
		{
			Lesson lesson;
			HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create (new Uri (lessonUrl)); 
			request.ContentType = "application/json";
			request.Method = "GET";

			using (WebResponse response = await request.GetResponseAsync ())
			{
				using (Stream stream = response.GetResponseStream ())
				{
					JsonValue jsonDoc = await Task.Run (() => JsonObject.Load (stream));
					lesson = JsonConvert.DeserializeObject<Lesson>(jsonDoc.ToString());
				}
			}
			return lesson;
		}

		/// <summary>Get a specific option.
		/// <para>Returns an option object from the option Rest url</para>
		/// </summary>
		public static async Task<Option> getOptionFromRest (string optionUrl)
		{
			Option option;
			HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create (new Uri (optionUrl)); 
			request.ContentType = "application/json";
			request.Method = "GET";

			using (WebResponse response = await request.GetResponseAsync ())
			{
				using (Stream stream = response.GetResponseStream ())
				{
					JsonValue jsonDoc = await Task.Run (() => JsonObject.Load (stream));
					option = JsonConvert.DeserializeObject<Option>(jsonDoc.ToString());
				}
			}
			return option;
		}
	}
}

