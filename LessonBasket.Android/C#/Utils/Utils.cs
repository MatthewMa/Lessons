using System;
using Android.Views;
using Android.Widget;
using Android.Content;
using Android.Runtime;
using Android.Content.Res;
using Android.Text.Format;
using Java.Util;
using Newtonsoft.Json;
using System.IO;
using Org.Json;
using System.Net;
using System.Json;
using System.Threading.Tasks;
using Android.Media;
using System.Collections.Generic;
using Android.OS;
using System.Security.Policy;
using Java.Net;
using Android.Graphics;

namespace LessonBasketDemo
{
	public class Utils
	{
		public static void findButtonSetOnClickListner (View root, View.IOnClickListener listener)
		{
			if (root is ViewGroup) {
				ViewGroup group = (ViewGroup)root;
				for (int i = 0; i < ((ViewGroup)root).ChildCount; i++) {
					View child = ((ViewGroup)root).GetChildAt (i);
					if (child is Button || child is ImageButton) {
						child.SetOnClickListener (listener);
					} else if (child is ViewGroup) {
						findButtonSetOnClickListner (child, listener);
					}
				}
			}
		}

		public static void showToastShort (String text, Context context)
		{
			Toast toast = Toast.MakeText (context, text, 0);
			toast.SetGravity (GravityFlags.Center, 0, 0);
			toast.Show ();
		}

		public static int getWindowWidth (Context context)
		{
			IWindowManager windowManager = context.GetSystemService (Context.WindowService).JavaCast<IWindowManager> ();
			return windowManager.DefaultDisplay.Width;
		}

		public static int getWindowHeight (Context context)
		{
			IWindowManager windowManager = context.GetSystemService (Context.WindowService).JavaCast<IWindowManager> ();
			return windowManager.DefaultDisplay.Height;
		}

		private static int ConvertPixelsToDp (Context context, float pixelValue)
		{
			var dp = (int)((pixelValue) / context.Resources.DisplayMetrics.Density);
			return dp;
		}

		public static string formatMillis (long time)
		{
			Calendar calendar = Calendar.Instance;
			calendar.Clear ();
			calendar.Add (Calendar.Millisecond, (int)time);
			String pattern = time / Constants.hourMillis > 0 ? "kk:mm:ss" : "mm:ss";
			return DateFormat.Format (pattern, calendar);
		}

		public static string ToJSON (object item)
		{
			var myval = JsonConvert.SerializeObject (item, Formatting.Indented);
			return myval;
		}

		public static T FromJSON<T> (string code)
		{
			var item = JsonConvert.DeserializeObject<T> (code);
			return item;
		}

		/// <summary>
		/// Gets the lecture from JSO.
		/// </summary>
		/// <returns>The lecture from JSO.</returns>
		/*public static List<Lecture> getLecturesFromJSON ()
		{
			//Here would be the request to fetch questions
			List<Lecture> lectures = new List<Lecture> ();
			JsonValue values = null;
			using (System.IO.Stream sr = Android.App.Application.Context.Assets.Open ("lecture.json")) {
				values = JsonObject.Load (sr);
			}
			if (values != null) {
				string json = values.ToString ();
				Lecture lecture = new Lecture (new JSONObject (json));
				lectures.Add (lecture);
			}
			return lectures;
		}*/

		/// <summary>
		/// Gets the string from JSO(just use for local json)
		/// </summary>
		/// <returns>The string from JSO.</returns>
		public static string getStringFromJSON ()
		{
			string json;
			using (StreamReader sr = new StreamReader (Android.App.Application.Context.Assets.Open ("lecture.json"))) {
				json = sr.ReadToEnd ();
			}
			return json;
		}

		/// <summary>
		/// Opens the audio.
		/// </summary>
		/// <param name="mp">Mp.</param>
		/// <param name="audioUri">Audio URI.</param>
		public static void openAudio (Context context, MediaPlayer mp, string audioUri)
		{
			if (!string.IsNullOrEmpty (audioUri)) {
				//close other music or video
				Intent i = new Intent ("com.android.music.musicservicecommand");
				i.PutExtra ("command", "pause");
				context.SendBroadcast (i);
			}
			try {
				mp.Completion += delegate(object sender, EventArgs e) {
					
				};
				mp.SetDataSource (context, Android.Net.Uri.Parse (EncodeURL (audioUri)));
				mp.PrepareAsync ();

			} catch (Exception ex) {
				ex.ToString ();
			}
		}

		public static void setAndPlayMusic (Context context, View view, string audioUrl, Handler handler, MediaPlayer mp)
		{
			Button btn_play;
			SeekBar sb_audio;
			TextView tv_play_time;
			SeekBar sb_voice;
			btn_play = view.FindViewById<Button> (Resource.Id.btn_play);
			sb_audio = view.FindViewById<SeekBar> (Resource.Id.sb_audio);
			tv_play_time = view.FindViewById<TextView> (Resource.Id.tv_play_time);
			sb_voice = view.FindViewById<SeekBar> (Resource.Id.sb_voice);
			Utils.openAudio (context, mp, audioUrl);
			mp.Prepared += delegate(object sender, EventArgs e) {
				//play
				mp.Start ();
				//update play button
				int id;
				if (mp.IsPlaying) {
					id = Resource.Drawable.selector_btn_pause;
				} else {
					id = Resource.Drawable.selector_btn_play;
				}
				btn_play.SetBackgroundResource (id);
				//set play-time
				tv_play_time.Text = Utils.formatMillis (mp.CurrentPosition);
				//set seekbar
				sb_audio.Max = mp.Duration;
				sb_audio.Progress = mp.CurrentPosition;
				//set volume
				AudioManager audioManager = (AudioManager)context.GetSystemService (Context.AudioService);
				int maxVolume = audioManager.GetStreamMaxVolume (Android.Media.Stream.Music);
				int currentVolume = audioManager.GetStreamVolume (Android.Media.Stream.Music);
				sb_voice.Max = maxVolume;
				sb_voice.Progress = currentVolume;
				//update
				sendUpdateTime (handler, tv_play_time, sb_audio, mp);
				//set voice
				sb_voice.ProgressChanged += delegate(object s, SeekBar.ProgressChangedEventArgs arg) {
					if (arg.FromUser) {
						audioManager.SetStreamVolume (Android.Media.Stream.Music, arg.Progress, VolumeNotificationFlags.PlaySound);
					}
				};
				//when the mediaplayer finish
				mp.Completion += delegate(object s, EventArgs arg) {
					mp.SeekTo (0);
					tv_play_time.Text = Utils.formatMillis (0L);
					btn_play.SetBackgroundResource (Resource.Drawable.selector_btn_play);
				};
			};
			//set onclick
			btn_play.Click += delegate(object sender, EventArgs e) {
				int id;
				if (mp.IsPlaying) {
					mp.Pause ();
					id = Resource.Drawable.selector_btn_play;
				} else {
					mp.Start ();
					id = Resource.Drawable.selector_btn_pause;
				}
				btn_play.SetBackgroundResource (id);

			};
		}

		public static void sendUpdateTime (Handler handler, TextView tv_play_time, SeekBar sb_audio, MediaPlayer mp)
		{
			handler.PostDelayed (delegate () {
				tv_play_time.Text = Utils.formatMillis (mp.CurrentPosition);
				sb_audio.Progress = mp.CurrentPosition;
				sendUpdateTime (handler, tv_play_time, sb_audio, mp);
			}, 300);
		}

		public static string EncodeURL (string str)
		{
			String url = str.Replace (" ", "%20");
			return url;
		}

		public static async Task setImageView (ImageView iv, string url)
		{
			Bitmap bitmap = null;
			HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create (EncodeURL (url));
			request.Method = "GET";
			request.ContentType = "multipart/form-data";
			using (WebResponse response = await request.GetResponseAsync ()) {
				using (System.IO.Stream stream = response.GetResponseStream ()) {
					bitmap = BitmapFactory.DecodeStream (stream);
				}
			}
			iv.SetImageBitmap (bitmap);
		}
	}

	public static class ObjectTypeHelper
	{
		public static T Cast<T> (this Java.Lang.Object obj) where T : class
		{
			var propertyInfo = obj.GetType ().GetProperty ("Instance");
			return propertyInfo == null ? null : propertyInfo.GetValue (obj, null) as T;
		}
	}

}

