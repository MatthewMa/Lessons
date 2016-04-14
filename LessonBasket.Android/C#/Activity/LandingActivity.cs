
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

namespace LessonBasketDemo
{
	[Activity (Label = "LessonBasketDemo", MainLauncher = true, ScreenOrientation = Android.Content.PM.ScreenOrientation.Portrait)]			
	public class LandingActivity : BaseActivity
	{
		private Button btn_start;

		public override void initListner ()
		{
			btn_start.Click += delegate(object sender, EventArgs e) {
				enterLoginActivity ();	
			};
		}

		public override void initData ()
		{
			
		}

		public override void initView ()
		{
			Handler handler = new Handler ();
			handler.PostDelayed (delegate() {
				enterLoginActivity ();
			}, 2000);
			btn_start = FindViewById<Button> (Resource.Id.btn_start);
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

