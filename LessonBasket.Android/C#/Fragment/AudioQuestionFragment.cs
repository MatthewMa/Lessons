
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
	public class AudioQuestionFragment : Fragment
	{
		public string text { get { return Arguments.GetString ("question", "Default Text"); } }

		public string audioUrl { get { return Arguments.GetString ("audio_url", "defaulturl"); } }

		public IList<string> questions { get { return Arguments.GetStringArrayList ("questions"); } }

		private MediaPlayer mp = new MediaPlayer ();

		public override Android.Views.View OnCreateView (Android.Views.LayoutInflater inflater, Android.Views.ViewGroup container, Android.OS.Bundle savedInstanceState)
		{
			base.OnCreateView (inflater, container, savedInstanceState);

			var view = inflater.Inflate (Resource.Layout.fragment_audioquestion, container, false);
			view.FindViewById<TextView> (Resource.Id.questionTxt).Text = text;
			populateChoices (view);
			Utils.setAndPlayMusic (Activity, view, audioUrl, QuestionnairActivity.handler, mp);
			return view;
		}

		public void populateChoices (View view)
		{

			ViewGroup choicesRadioGroup = (ViewGroup)view.FindViewById<RadioGroup> (Resource.Id.choicesRadioGrp);

			for (int i = 0; i < questions.Count; i++) {
				LessonBasket.Option option = null;
				//go to server
				try {
					option = LessonUtil.getOptionFromRest (questions [i]).Result;
				} catch {
					DialogFactory.ToastDialog (this.Activity, "Data Error", "Data error, please try again!", 5);
				}
				if (option != null) {
					RadioButton rdBtn = new RadioButton (Application.Context);
					rdBtn.Id = (option.order);
					rdBtn.Text = option.title;
					choicesRadioGroup.AddView (rdBtn);
				}
			}		
		}

		public static AudioQuestionFragment NewInstance (Screen screen)
		{
			var audioQuestionFrag = new AudioQuestionFragment{ Arguments = new Bundle () };
			audioQuestionFrag.Arguments.PutString ("question", screen.question);
			audioQuestionFrag.Arguments.PutString ("audio_url", screen.audio_url);
			audioQuestionFrag.Arguments.PutStringArrayList ("questions", screen.questions);
			return audioQuestionFrag;
		}

		public override void OnDestroy ()
		{
			mp.Stop ();
			QuestionnairActivity.handler.RemoveCallbacksAndMessages (null);
			base.OnDestroy ();
		}
	}
}

