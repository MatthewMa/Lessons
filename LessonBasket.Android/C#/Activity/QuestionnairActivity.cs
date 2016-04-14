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

namespace LessonBasketDemo
{
	[Activity (Label = "LessonBasketDemo")]
	public class QuestionnairActivity : Activity
	{
		private Button nextBtn;
		private Button previousBtn;
		private int currentScreen = 1;
		private int position = 0;
		//which video info it is
		private Lecture lecture;
		private Fragment fragment;
		private Button btn_submit;
		private ImageView iv_vision;
		public static Handler handler = new Handler ();

		protected override void OnCreate (Bundle savedInstanceState)
		{
			base.OnCreate (savedInstanceState);
			SetContentView (Resource.Layout.activity_questionnair);
			nextBtn = FindViewById<Button> (Resource.Id.btn_next);
			previousBtn = FindViewById<Button> (Resource.Id.btn_pre);
			fragment = FragmentManager.FindFragmentById (Resource.Id.fragmentContainer);
			btn_submit = FindViewById<Button> (Resource.Id.btn_submit);
			iv_vision = FindViewById<ImageView> (Resource.Id.iv_vision);
			//start animation
			var animation = (AnimationDrawable)iv_vision.Background;
			animation.Start ();
			//receive position
			position = Intent.GetIntExtra ("index", 0);
			//set lecture
			lecture = Constants.lists [position];
			updateScreen ();
			validateBtns ();
			nextBtn.Click += NextBtn_Click;
			previousBtn.Click += PreviousBtn_Click;
			btn_submit.Click += delegate(object sender, EventArgs e) {
				//submit button is pressed
				submitAnswers ();
			};
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
			if (currentScreen < Constants.lists [position].screenCount - 1)
				currentScreen++;
			validateBtns ();
			updateScreen ();
			handler.RemoveCallbacksAndMessages (null);
		}

		private void updateScreen ()
		{
			//Every time a button is clicked the screen is updated
			FindViewById<TextView> (Resource.Id.navigationTxt).Text = currentScreen + " / " + (lecture.screenCount - 1);

			switch (lecture.screens [currentScreen].type) {

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

			case "video":
				//to do:if there is video question
				break;

			default:
				break;
			}
			
		}

		private void showAudioEditTextFragment ()
		{
			
			var audioEditText = fragment as AudioEditTextFragment;


			if (audioEditText == null) {
				// Make new fragment to show this selection.
				audioEditText = AudioEditTextFragment.NewInstance (lecture.screens [currentScreen]);

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
				audioQuestion = AudioQuestionFragment.NewInstance (lecture.screens [currentScreen]);

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
				audioQuestionImage = AudioQuestionImageFragment.NewInstance (lecture.screens [currentScreen]);

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
				audioText = AudioTextFragment.NewInstance (lecture.screens [currentScreen]);

				// Execute a transaction, replacing any existing
				// fragment with this one inside the frame.
				var ft = FragmentManager.BeginTransaction ();
				ft.Replace (Resource.Id.fragmentContainer, audioText);
				ft.SetTransition (FragmentTransit.FragmentFade);
				ft.Commit ();
			}

		}

		private void validateBtns ()
		{
			previousBtn.Visibility = ViewStates.Visible;
			nextBtn.Visibility = ViewStates.Visible;
			btn_submit.Visibility = ViewStates.Invisible;
			if (currentScreen == 1) {
				previousBtn.Visibility = ViewStates.Invisible;
			} else if (lecture != null && currentScreen == lecture.screenCount - 1) {
				nextBtn.Visibility = ViewStates.Invisible;
				btn_submit.Visibility = ViewStates.Visible;
			}
				
		}


		private Boolean updateAnswers ()
		{
			//Every time a next or previous button is clicked, the answer set in that screen is saved
			String answer = "";
			switch (lecture.screens [currentScreen].type) {

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
}


