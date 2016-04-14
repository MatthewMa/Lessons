
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
using Android.Support.V4.View;
using System.Threading;

namespace LessonBasketDemo
{
	[Activity (Label = "LessonBasketDemo", ScreenOrientation = Android.Content.PM.ScreenOrientation.Portrait)]			
	public class IndexActivity : BaseActivity
	{
		private string email;
		private TextView tv_title;
		private GridView gv;
		private TextView tv_des;
		private LinearLayout ll;
		private ImageButton btn_setting;
		private ImageButton btn_exit;
		private IO.Vov.Vitamio.Widget.VideoView vv;
		//json string
		public readonly string[] titles = {
			"Personal Account",
			"Getting Started",
			"Learn More",
			"Buy Lessons",
			"Result Record",
			"Email Us"
		};
		public readonly int[] resources = {
			Resource.Drawable.myspace,
			Resource.Drawable.forward,
			Resource.Drawable.search,
			Resource.Drawable.cart,
			Resource.Drawable.pencil,
			Resource.Drawable.mouse
		};

		public override void initListner ()
		{
			//set item  listener for gridview
			gv.ItemClick += delegate(object sender, AdapterView.ItemClickEventArgs e) {
				switch (e.Position) {
				case 0://personal account

					break;
				case 1://getting started
					getStart ();
					break;
				case 2://learn more

					break;
				case 3://buy lessons

					break;
				case 4://result record

					break;
				case 5://email us

					break;
		
				}
			};
		}

		private void getStart ()
		{
			Intent intent = new Intent (this, typeof(HomeActivity));
			StartActivity (intent);

		}

		public override void initData ()
		{
			//receiver data
			email = Intent.GetStringExtra ("email");
			if (!string.IsNullOrEmpty (email)) {
				tv_title.Text = "Welcome to Lesson Basket," + email;
			}
			MyGridViewAdapter my = new MyGridViewAdapter (titles, resources, this);
			gv.Adapter = my;
			//set videoview
			vv.SetVideoURI (Android.Net.Uri.Parse (Constants.VIDEO_URL));
			vv.RequestFocus ();
			vv.Prepared += delegate(object sender, IO.Vov.Vitamio.MediaPlayer.PreparedEventArgs e) {
				e.P0.SetPlaybackSpeed (1.0f);
				e.P0.Looping = false;
				vv.Start ();
			};
			vv.Completion += delegate(object sender, IO.Vov.Vitamio.MediaPlayer.CompletionEventArgs e) {
				e.P0.Stop ();
			};
		}

		public override void initView ()
		{
			tv_title = FindViewById<TextView> (Resource.Id.tv_title);
			gv = FindViewById<GridView> (Resource.Id.gv_index);
			btn_setting = FindViewById<ImageButton> (Resource.Id.btn_setting);
			btn_exit = FindViewById<ImageButton> (Resource.Id.btn_exit);
			vv = FindViewById<IO.Vov.Vitamio.Widget.VideoView> (Resource.Id.video_view);
		}

		public override int getLayoutResource ()
		{
			return Resource.Layout.activity_index;
		}

		public override void onClick (View v, int id)
		{
			switch (id) {
			case Resource.Id.btn_setting://setting 

				break;
			case Resource.Id.btn_exit://exit
				DialogFactory.toastNegativePositiveDialog (this, "Log out", "Are you sure to log out?", 3);
				break;
			}
		}

		public override void OnBackPressed ()
		{
			//back pressed
		}
	}
}

