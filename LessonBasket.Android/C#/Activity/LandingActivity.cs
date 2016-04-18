
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Views;
using Android.Widget;
using Java.Lang;

namespace LessonBasketDemo
{
	[Activity (Label = "LessonBasketDemo", MainLauncher = true, ScreenOrientation = Android.Content.PM.ScreenOrientation.Portrait)]			
	public class LandingActivity : BaseActivity
	{
		private Button btn_start;
		private Handler handler = new Handler ();
		private TextView tv_maintitle;
		private TextView tv_subtitle;

		public override void initListner ()
		{
			btn_start.Click += delegate(object sender, EventArgs e) {

				handler.RemoveCallbacksAndMessages (null);
				enterLoginActivity ();	
			};
		}

		public override void initData ()
		{
			handler.PostDelayed (delegate() {
				enterLoginActivity ();
			}, 5000);
		}

		public override void initView ()
		{
			btn_start = FindViewById<Button> (Resource.Id.btn_start);
			tv_maintitle = FindViewById<TextView> (Resource.Id.tv_maintitle);
			tv_subtitle = FindViewById<TextView> (Resource.Id.tv_subtitle);
			NineOldAndroids.View.ViewPropertyAnimator.Animate (tv_maintitle).SetDuration (1200).Alpha (1.0f).Start ();
			handler.PostDelayed (delegate() {
				NineOldAndroids.View.ViewPropertyAnimator.Animate (tv_subtitle).SetDuration (1200).Alpha (1.0f).Start ();
			}, 1200);
			handler.PostDelayed (delegate() {
				btn_start.Visibility = ViewStates.Visible;
				NineOldAndroids.View.ViewPropertyAnimator.Animate (btn_start).SetDuration (1500).TranslationY (-150).Start ();
			}, 1200);
		}

		/// <summary>
		/// Enters the login activity.
		/// </summary>
		void enterLoginActivity ()
		{
			StartActivity (new Intent (this, typeof(LoginActivity)));
			Finish ();
		}

		public override int getLayoutResource ()
		{
			return Resource.Layout.activity_landing;
		}

		public override void onClick (View v, int id)
		{
			
		}

		public override void OnBackPressed ()
		{
			
		}
	}
}

