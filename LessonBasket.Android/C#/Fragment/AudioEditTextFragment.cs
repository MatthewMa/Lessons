
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
using System.Threading.Tasks;

namespace LessonBasketDemo
{
	public class AudioEditTextFragment : Fragment
	{
		MediaPlayer mp = new MediaPlayer ();

		public string text { get { return Arguments.GetString ("text", "Default Text"); } }

		public string audioUrl { get { return Arguments.GetString ("audio_url", "defaulturl"); } }

		private static List<LessonBasket.Image> images;

		private ImageView iv1;

		private ImageView iv2;

		private TextView subtxt1;

		private TextView subtxt2;

		public override  Android.Views.View OnCreateView (Android.Views.LayoutInflater inflater, Android.Views.ViewGroup container, Android.OS.Bundle savedInstanceState)
		{
			base.OnCreateView (inflater, container, savedInstanceState);
			var view = inflater.Inflate (Resource.Layout.fragment_audioedittext, container, false);
			view.FindViewById<TextView> (Resource.Id.questionTxt).Text = text;
			iv1 = view.FindViewById<ImageView> (Resource.Id.imageView1);
			iv2 = view.FindViewById<ImageView> (Resource.Id.imageView2);
			subtxt1 = view.FindViewById<TextView> (Resource.Id.subTxt1);
			subtxt2 = view.FindViewById<TextView> (Resource.Id.subTxt2);
			Utils.setImageView (iv1, images [0].url);
			subtxt1.Text = images [0].title;
			Utils.setImageView (iv2, images [1].url);
			subtxt2.Text = images [1].title;
			Utils.setAndPlayMusic (Activity, view, audioUrl, QuestionnairActivity.handler, mp);
			return view;
		}


		public static AudioEditTextFragment NewInstance (Screen screen)
		{
			var audioEditTextFragment = new AudioEditTextFragment{ Arguments = new Bundle () };
			audioEditTextFragment.Arguments.PutString ("text", screen.text);
			audioEditTextFragment.Arguments.PutString ("audio_url", screen.audio_url);
			if (screen != null) {
				images = new List<LessonBasket.Image> (screen.images);
			}
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

