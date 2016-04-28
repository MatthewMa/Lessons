
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Media;
using LessonBasket;

namespace LessonBasketDemo
{
	[Activity (Label = "AudioText")]			
	public class AudioTextFragment : Fragment
	{
		private MediaPlayer mp = new MediaPlayer ();

		public string text { get { return Arguments.GetString ("text", "Default Text"); } }

		public string audioUrl { get { return Arguments.GetString ("audio_url", "defaulturl"); } }
		/*protected override void OnCreate (Bundle savedInstanceState)
		{
			base.OnCreate (savedInstanceState);
			//SetContentView (Resource.Layout.AudioText);

			// Create your application here
		}
*/

		public override Android.Views.View OnCreateView (Android.Views.LayoutInflater inflater, Android.Views.ViewGroup container, Android.OS.Bundle savedInstanceState)
		{
			base.OnCreateView (inflater, container, savedInstanceState);
			var view = inflater.Inflate (Resource.Layout.fragment_audiotext, container, false);
			view.FindViewById<TextView> (Resource.Id.displayText).Text = text;
			Utils.setAndPlayMusic (Activity, view, audioUrl, QuestionnairActivity.handler, mp);
			return view;
		}

		public static AudioTextFragment NewInstance (Screen screen)
		{
			var audioTextFrag = new AudioTextFragment{ Arguments = new Bundle () };
			audioTextFrag.Arguments.PutString ("text", screen.Text);
			audioTextFrag.Arguments.PutString ("audio_url", screen.AudioUrl);
			return audioTextFrag;
		}

		public override void OnDestroy ()
		{
			QuestionnairActivity.handler.RemoveCallbacksAndMessages (null);//remove all messages
			mp.Stop ();
			base.OnDestroy ();
		}
	}
}

