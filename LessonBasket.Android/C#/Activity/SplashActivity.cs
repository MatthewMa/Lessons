
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
		public override void initData ()
		{

			//getDataFromServer ();
			//get data from local file

			try {
				Constants.lists = Utils.getLecturesFromJSON ();
			} catch (Exception ex) {
				returnLogin ();
			}
			delayEnterHome (3000);
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

		/// <summary>
		/// Gets the lecture from server(new thread).
		/// </summary>
		private async void getDataFromServer ()
		{
			HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create (new Uri (Constants.server)); //TODO This url should not be hardcoded
			request.ContentType = "application/json";
			request.Timeout = 5000;//set connection timeout
			request.ReadWriteTimeout = 5000;//set readwrite timeout
			request.Method = "GET";
			checkVersion ();//to do:version check

			try {
				using (HttpWebResponse response = (HttpWebResponse)await request.GetResponseAsync ()) {
					if (response.StatusCode == HttpStatusCode.OK) {//checke if the response successful
						// Get a stream representation of the HTTP web response:
						using (Stream stream = response.GetResponseStream ()) {
							long timestarted = DateTime.Now.Millisecond;
							// Use this stream to build a JSON document object:
							JsonValue jsonDoc = await Task.Run (() => JsonObject.Load (stream));
							try {
								for (int i = 0; i < jsonDoc.Count; i++) {
									string json = jsonDoc.ToString ();
									Lecture lecture = new Lecture (new JSONObject (json));
									Constants.lists = new List<Lecture> ();
									Constants.lists.Add (lecture);//save lecture to list
								}
								//to do: create a local database to save the JSON object
								if (Constants.lists.Count > 0) {
									long timeended = DateTime.Now.Millisecond;
									long timeused = timeended - timestarted;
									if (timeused < 3000) {
										delayEnterHome (3000 - timeused);
									}
								}
							} catch (Exception ex) { //catch JSON PARSING ERROR
								returnLogin ();
							}
						} 
					} else {
						//get error,send message
						Message msg = handler.ObtainMessage ();
						msg.What = Constants.REQUEST_FAILED;
						handler.SendMessage (msg);
					}
				}
			} catch (Exception ex) {
				//NET ERROR
				Message msg = handler.ObtainMessage ();
				msg.What = Constants.NET_ERROR;
				handler.SendMessage (msg);
			}		
		}

		private void returnLogin ()
		{
			DialogFactory.ToastDialog (this, "Data Error", "Cannot parse data,please try again!", 1);
		}
	}
}

