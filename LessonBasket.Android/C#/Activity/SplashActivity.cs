
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
using System.Threading;
using System.Net;
using System.IO;
using System.Json;
using System.Threading.Tasks;
using Org.Json;
using System.ComponentModel;
using LessonBasket;

namespace LessonBasketDemo
{
	[Activity (Label = "LessonBasketDemo")]			
	public class SplashActivity : BaseActivity
	{

		private MyUIHandler handler;

		/// <summary>
		/// Initiate the listner
		/// </summary>
		public override void initListner ()
		{
			
		}

		/// <summary>
		/// initiate data
		/// </summary>
		public override async void initData ()
		{

			//getDataFromServer ();
			//get data from local file
			try {
				var tem = await LessonUtil.GetLessonsAsync ();
				IList<Lesson> urls = tem;
				Constants.lessons_url = new List<Lesson> (urls);//get lessons
			} catch (Exception ex) {
				returnLogin ();
			}
			if (Constants.lessons_url != null) {
				delayEnterHome (3000);
			}
		}

		/// <summary>
		/// Checks the version.
		/// </summary>
		private void checkVersion ()
		{
			
		}

		/// <summary>
		/// Inits the view.
		/// </summary>
		public override void initView ()
		{
			handler = new MyUIHandler (this);
		}

		/// <summary>
		/// Gets the layout resource.
		/// </summary>
		/// <returns>The layout resource.</returns>
		public override int getLayoutResource ()
		{
			return Resource.Layout.activity_splash;
		}

		public override void onClick (View v, int id)
		{
			
		}

		private void delayEnterHome (long time)
		{
			handler.PostDelayed (delegate {
				enterHome ();
			}, time);
		}

		private void enterHome ()
		{
			string email = Intent.GetStringExtra ("email");
			Intent intent = new Intent (this, typeof(IndexActivity));
			if (!string.IsNullOrEmpty (email)) {
				intent.PutExtra ("email", email);
			}
			Toast.MakeText (this, "Welcome,Login successfully!", ToastLength.Short).Show ();
			StartActivity (intent);
			Finish ();
		}

		public override void OnBackPressed ()
		{
			
		}

		public override bool OnTouchEvent (MotionEvent e)
		{
			switch (e.Action) {
			case MotionEventActions.Down:
				enterHome ();
				handler.RemoveCallbacksAndMessages (null);
				break;

			}
			return base.OnTouchEvent (e);
		}

		private void returnLogin ()
		{
			DialogFactory.ToastDialog (this, "Server Error", "Cannot connect to server,please try again!", 1);
		}
	}
}

