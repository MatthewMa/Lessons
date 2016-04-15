
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using Android.Media;
using LessonBasket;

namespace LessonBasketDemo
{
	public class AudioEditTextFragment : Fragment
	{
		MediaPlayer mp = new MediaPlayer ();

		public string text { get { return Arguments.GetString ("text", "Default Text"); } }

		public string audioUrl { get { return Arguments.GetString ("audio_url", "defaulturl"); } }

		public override Android.Views.View OnCreateView (Android.Views.LayoutInflater inflater, Android.Views.ViewGroup container, Android.OS.Bundle savedInstanceState)
		{
			base.OnCreateView (inflater, container, savedInstanceState);

			var view = inflater.Inflate (Resource.Layout.fragment_audioedittext, container, false);
			view.FindViewById<TextView> (Resource.Id.questionTxt).Text = text;
			Utils.setAndPlayMusic (Activity, view, audioUrl, QuestionnairActivity.handler, mp);
			return view;
		}


		public static AudioEditTextFragment NewInstance (Screen screen)
		{
			var audioEditTextFragment = new AudioEditTextFragment{ Arguments = new Bundle () };
			audioEditTextFragment.Arguments.PutString ("text", screen.text);
			audioEditTextFragment.Arguments.PutString ("audio_url", screen.audioUrl);
			return audioEditTextFragment;
		}

		public override void OnDestroy ()
		{			
			QuestionnairActivity.handler.RemoveCallbacksAndMessages (null);//remove all messages
			mp.Stop ();
			base.OnDestroy ();
		}
	}
}

