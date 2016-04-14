
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
	public class IndexActivity : BaseActivity,ViewPager.IOnTouchListener
	{
		private MyIndexHandler handler;
		private string email;
		private TextView tv_title;
		private GridView gv;
		private TextView tv_des;
		private LinearLayout ll;
		private int previousPosition = 0;
		private ImageButton btn_setting;
		private ImageButton btn_exit;
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
		public readonly int[] lessonresources = {
			Resource.Drawable.lesson_1,
			Resource.Drawable.lesson_2,
			Resource.Drawable.lesson_3,
			Resource.Drawable.lesson_4,
			Resource.Drawable.lesson_5
		};
		public readonly string[] description = { 
			"Geiafitness now 25% OFF",
			"Quick Fix 245",
			"Fruit and Vegatable carving",
			"Fitness and Fun",
			"HC Varsity"
		};
		private ViewPager vp;

		public override void initListner ()
		{
			vp.SetOnTouchListener (this);
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
			//set adapter for viewpager
			MyPagerAdapter mp = new MyPagerAdapter (lessonresources);
			vp.Adapter = mp;
			//set current position
			vp.SetCurrentItem (lessonresources.Length * 10000, true);
			vp.PageSelected += delegate(object sender, ViewPager.PageSelectedEventArgs e) {
				int p = (e.Position % lessonresources.Length);
				tv_des.Text = description [p];
				ll.GetChildAt (p).Enabled = true;
				ll.GetChildAt (previousPosition).Enabled = false;
				previousPosition = p;
			};
			tv_des.Text = description [0];
			handler = new MyIndexHandler (vp);
			handler.SendEmptyMessageDelayed (0, 2000);

		}

		public override void initView ()
		{
			tv_title = FindViewById<TextView> (Resource.Id.tv_title);
			gv = FindViewById<GridView> (Resource.Id.gv_index);
			vp = FindViewById<ViewPager> (Resource.Id.vv_advertiser);
			tv_des = FindViewById<TextView> (Resource.Id.tv_des);
			ll = FindViewById<LinearLayout> (Resource.Id.ll_container);
			btn_setting = FindViewById<ImageButton> (Resource.Id.btn_setting);
			btn_exit = FindViewById<ImageButton> (Resource.Id.btn_exit);
			//dynamically create the circle image
			for (int i = 0; i < lessonresources.Length; i++) {
				ImageView iv = new ImageView (this);
				iv.SetImageResource (Resource.Drawable.selector_point);
				//set left margin
				if (i > 0) {
					LinearLayout.LayoutParams param = new LinearLayout.LayoutParams (LinearLayout.LayoutParams.WrapContent, LinearLayout.LayoutParams.WrapContent);
					param.LeftMargin = 6;
					iv.Enabled = false;
					iv.LayoutParameters = param;
				}
				ll.AddView (iv);
			}
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

		//Touch Listener
		public bool OnTouch (View v, MotionEvent e)
		{
			switch (e.Action) {

			case MotionEventActions.Down:
				//stop send message
				handler.RemoveCallbacksAndMessages (null);
				break;
			case MotionEventActions.Up:
				handler.SendEmptyMessageDelayed (0, 2000);
				break;
			}
			return false;
		}

		public override void OnBackPressed ()
		{
			//back pressed
		}
	}

	public class MyIndexHandler:Handler
	{
		private ViewPager mvp;

		public MyIndexHandler (ViewPager vp)
		{
			this.mvp = vp;
		}

		public override void HandleMessage (Message msg)
		{
			//update next page automatically
			int currentItem = mvp.CurrentItem;
			currentItem++;
			mvp.SetCurrentItem (currentItem, true);
			SendEmptyMessageDelayed (0, 2000);
		}
	}
}

