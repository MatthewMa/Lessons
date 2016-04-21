
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
	public class AudioQuestionImageFragment : Fragment
	{

		public string text { get { return Arguments.GetString ("question", "Default Text"); } }

		public string audioUrl { get { return Arguments.GetString ("audio_url", "defaulturl"); } }

		private static List<LessonBasket.Option> options;

		private static List<LessonBasket.Image> images;

		private MediaPlayer mp = new MediaPlayer ();

		private ImageView iv;


		public override Android.Views.View OnCreateView (Android.Views.LayoutInflater inflater, Android.Views.ViewGroup container, Android.OS.Bundle savedInstanceState)
		{
			base.OnCreateView (inflater, container, savedInstanceState);

			var view = inflater.Inflate (Resource.Layout.fragment_audioquestionimage, container, false);
			view.FindViewById<TextView> (Resource.Id.questionTxt).Text = text;
			iv = view.FindViewById<ImageView> (Resource.Id.imageView1);
			Utils.setImageView (iv, images [0].url);
			populateChoices (view);
			Utils.setAndPlayMusic (Activity, view, audioUrl, QuestionnairActivity.handler, mp);
			return view;
		}

		public void populateChoices (View view)
		{

			ViewGroup choicesRadioGroup = (ViewGroup)view.FindViewById<RadioGroup> (Resource.Id.choicesRadioGrp);


			for (int i = 0; i < options.Count; i++) {
				RadioButton rdBtn = new RadioButton (Application.Context);
				rdBtn.Id = (i);
				rdBtn.Text = options [i].title;
				rdBtn.Gravity = GravityFlags.Center;
				choicesRadioGroup.AddView (rdBtn);
			}

			
		}

		public static AudioQuestionImageFragment NewInstance (Screen screen)
		{
			var audioQuestionImageFrag = new AudioQuestionImageFragment{ Arguments = new Bundle () };
			audioQuestionImageFrag.Arguments.PutString ("question", screen.question);
			audioQuestionImageFrag.Arguments.PutString ("audio_url", screen.audio_url);
			options = new List<LessonBasket.Option> (screen.options);
			images = new List<LessonBasket.Image> (screen.images);
			return audioQuestionImageFrag;
		}

		public override void OnDestroy ()
		{
			mp.Stop ();
			QuestionnairActivity.handler.RemoveCallbacksAndMessages (null);
			base.OnDestroy ();
		}
	}
}

