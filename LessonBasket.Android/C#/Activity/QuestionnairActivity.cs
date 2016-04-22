using Android.App;
using Android.Widget;
using Android.OS;
using System.IO;
using Org.Json;
using System.Net;
using System;
using System.Json;
using System.Threading.Tasks;
using Android.Content;
using Android.Views;
using Android.Graphics.Drawables;
using System.Collections.Generic;
using LessonBasket;
using Android.Graphics;

namespace LessonBasketDemo
{
	[Activity (Label = "LessonBasketDemo")]
	public class QuestionnairActivity : Activity
	{
		private Button nextBtn;
		private Button previousBtn;
		private int currentScreen = 1;
		private int position = 0;
		private Fragment fragment;
		private Button btn_submit;
		private ImageView iv_vision;
		private List<Screen> screens;
		private Screen screen;
		public static Handler handler = new Handler ();
		private LinearLayout ll_loading;
		private LinearLayout ll_questions;
		public TextView tv_timer;
		private int duration;

		protected override async void OnCreate (Bundle savedInstanceState)
		{
			base.OnCreate (savedInstanceState);
			SetContentView (Resource.Layout.activity_questionnair);
			nextBtn = FindViewById<Button> (Resource.Id.btn_next);
			previousBtn = FindViewById<Button> (Resource.Id.btn_pre);
			fragment = FragmentManager.FindFragmentById (Resource.Id.fragmentContainer);
			btn_submit = FindViewById<Button> (Resource.Id.btn_submit);
			ll_loading = FindViewById<LinearLayout> (Resource.Id.ll_loading);
			ll_questions = FindViewById<LinearLayout> (Resource.Id.ll_questions);
			tv_timer = FindViewById<TextView> (Resource.Id.tv_timer);
			ll_loading.Visibility = ViewStates.Visible;
			ll_questions.Visibility = ViewStates.Gone;
			//receive position
			position = Intent.GetIntExtra ("index", 0);
			//set total time
			duration = 60 * Constants.lessons_url [position].screenCount * 1000;
			//catch exception
			try {
				screens = await LessonUtil.GetScreensByLessonAsync (position + 1);
			} catch (Exception ex) {
				DialogFactory.ToastDialog (this, "Data Error", "Data error,please try again later!", 4);
			}
			updateScreen ();
			validateBtns ();
			nextBtn.Click += NextBtn_Click;
			previousBtn.Click += PreviousBtn_Click;
			btn_submit.Click += delegate(object sender, EventArgs e) {
				//submit button is pressed
				submitAnswers ();
			};
			ll_loading.Visibility = ViewStates.Gone;
			ll_questions.Visibility = ViewStates.Visible;
			//start count time:
			MyCountDownTimer timer = new MyCountDownTimer (duration, 1000, this);
			timer.Start ();
		}

		/// <summary>
		/// Submits the answers.
		/// </summary>
		private void submitAnswers ()
		{
			//toast a dialog
			DialogFactory.toastNegativePositiveDialog (this, "Submit", "Are you sure to submit the answers", 2);
		}

		void PreviousBtn_Click (object sender, System.EventArgs e)
		{
			

			updateAnswers (); //TODO if there is no answer, the flow should stop and the user must answer the question
			if (currentScreen > 1)
				currentScreen--;
			validateBtns ();
			updateScreen ();
			handler.RemoveCallbacksAndMessages (null);
		}

		void NextBtn_Click (object sender, System.EventArgs e)
		{
			

			updateAnswers (); //TODO if there is no answer, the flow should stop and the user must answer the question
			if (currentScreen < screens.Count - 1)
				currentScreen++;
			validateBtns ();
			updateScreen ();
			handler.RemoveCallbacksAndMessages (null);
		}

		private void updateScreen ()
		{
			//Every time a button is clicked the screen is updated
			FindViewById<TextView> (Resource.Id.navigationTxt).Text = currentScreen + " / " + (screens.Count - 1);
			//get screen
			screen = screens [currentScreen];
			if (screen != null) {
				switch (screen.Type) {

				case "audio_text":
					showAudioTextFragment ();
					break;

				case "audio_question_image":
					showAudioQuestionImageFragment ();
					break;

				case "audio_question":
					showAudioQuestionFragment ();
					break;

				case "audio_edittext":
					showAudioEditTextFragment ();
					break;
				case "text":
					showTextFragment ();
					break;
				case "audio_image":
					showAudioImageFragment ();
					break;
				case "audio_text_image":
					showAudioTextImage ();
					break;
				case "audio_text_image_edittext":
					showAudioTextImageEdittext ();
					break;
				case "video":
				//to do:if there is video question
					showVideoFragment ();
					break;
				default:
					break;
				}
			}
			
		}

		private void showAudioEditTextFragment ()
		{
			
			var audioEditText = fragment as AudioEditTextFragment;


			if (audioEditText == null) {
				// Make new fragment to show this selection.
				audioEditText = AudioEditTextFragment.NewInstance (screen);

				// Execute a transaction, replacing any existing
				// fragment with this one inside the frame.
				var ft = FragmentManager.BeginTransaction ();
				ft.Replace (Resource.Id.fragmentContainer, audioEditText);
				ft.SetTransition (FragmentTransit.FragmentFade);
				ft.Commit ();
			}
		}

		private void showAudioQuestionFragment ()
		{

			var audioQuestion = fragment as AudioQuestionFragment;


			if (audioQuestion == null) {
				// Make new fragment to show this selection.
				audioQuestion = AudioQuestionFragment.NewInstance (screen);

				// Execute a transaction, replacing any existing
				// fragment with this one inside the frame.
				var ft = FragmentManager.BeginTransaction ();
				ft.Replace (Resource.Id.fragmentContainer, audioQuestion);
				ft.SetTransition (FragmentTransit.FragmentFade);
				ft.Commit ();
			}
		}

		private void showAudioQuestionImageFragment ()
		{
			
			var audioQuestionImage = fragment as AudioQuestionImageFragment;


			if (audioQuestionImage == null) {
				// Make new fragment to show this selection.
				audioQuestionImage = AudioQuestionImageFragment.NewInstance (screen);

				// Execute a transaction, replacing any existing
				// fragment with this one inside the frame.
				var ft = FragmentManager.BeginTransaction ();
				ft.Replace (Resource.Id.fragmentContainer, audioQuestionImage);
				ft.SetTransition (FragmentTransit.FragmentFade);
				ft.Commit ();
			}
		}

		private void showAudioTextFragment ()
		{
			var audioText = fragment as AudioTextFragment;


			if (audioText == null) {
				// Make new fragment to show this selection.
				audioText = AudioTextFragment.NewInstance (screen);

				// Execute a transaction, replacing any existing
				// fragment with this one inside the frame.
				var ft = FragmentManager.BeginTransaction ();
				ft.Replace (Resource.Id.fragmentContainer, audioText);
				ft.SetTransition (FragmentTransit.FragmentFade);
				ft.Commit ();
			}

		}

		private void showTextFragment ()
		{
			var text = fragment as TextFragment;
			if (text == null) {
				// Make new fragment to show this selection.
				text = TextFragment.NewInstance (screen);

				// Execute a transaction, replacing any existing
				// fragment with this one inside the frame.
				var ft = FragmentManager.BeginTransaction ();
				ft.Replace (Resource.Id.fragmentContainer, text);
				ft.SetTransition (FragmentTransit.FragmentFade);
				ft.Commit ();
			}
		}

		private void showVideoFragment ()
		{
			
		}

		private void showAudioImageFragment ()
		{
			
		}

		private void showAudioTextImage ()
		{
			
		}

		private void showAudioTextImageEdittext ()
		{
			
		}

		private void validateBtns ()
		{
			previousBtn.Visibility = ViewStates.Visible;
			nextBtn.Visibility = ViewStates.Visible;
			btn_submit.Visibility = ViewStates.Invisible;
			if (currentScreen == 1) {
				previousBtn.Visibility = ViewStates.Invisible;
			} else if (screen != null && currentScreen == screens.Count - 1) {
				nextBtn.Visibility = ViewStates.Invisible;
				btn_submit.Visibility = ViewStates.Visible;
			}
				
		}


		private Boolean updateAnswers ()
		{
			//Every time a next or previous button is clicked, the answer set in that screen is saved
			String answer = "";
			switch (screen.Type) {

			case "audio_text":
				
				return true;

			case "audio_question_image":
				if (FindViewById<RadioGroup> (Resource.Id.choicesRadioGrp).CheckedRadioButtonId == -1)
					//If the user has not selected any radiobutton we return false
					return false;
				answer = FindViewById<RadioButton> (FindViewById<RadioGroup> (Resource.Id.choicesRadioGrp).CheckedRadioButtonId).Text;
				return true;

			case "audio_question":

				return true;

			case "audio_edittext":

				return true;

			case "video":
				
				return true;
			case "text":

				return true;
			default:
				
				return true;
			}

		}

		public override void OnBackPressed ()
		{
			//back pressed
			DialogFactory.ToastDialog (this, "Return", "Please answer all questions and submit!", 0);
		}
	}

	public class MyCountDownTimer:CountDownTimer
	{
		private Context context;

		public MyCountDownTimer (long millisInFuture, long countDownInterval, Context context) : base (millisInFuture, countDownInterval)
		{
			this.context = context;
		}

		public override void OnFinish ()
		{
			DialogFactory.ToastDialog (context, "Time up", "Time is out,it will go to result screen!", 6);
		}

		public override void OnTick (long millisUntilFinished)
		{
			if (millisUntilFinished <= 10000) {
				(context as QuestionnairActivity).tv_timer.SetTextColor (Color.Red);
			}
			(context as QuestionnairActivity).tv_timer.Text = Utils.formatMillis (millisUntilFinished) + "";
		}
	}
}


