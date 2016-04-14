using Android.Widget;
using Android.OS;
using IO.Vov.Vitamio.Widget;
using Android.Views;
using Android.Support.V4.View;
using System.Collections.Generic;
using Android.Content;
using NineOldAndroids.View;
using Android.Support.V4.App;
using Android.App;

namespace LessonBasketDemo
{
	[Activity (Label = "LessonBasketDemo")]
	public class HomeActivity : BaseActivity
	{
		private TextView tv_video;
		private TextView tv_audio;
		private View v_indicator;
		private ViewPager vp;
		private List<Android.Support.V4.App.Fragment> lists;
		private int pageSize;
		private int width;
		private LinearLayout ll_frame;

		public override void initView ()
		{
			tv_audio = FindViewById<TextView> (Resource.Id.tv_downlessons);
			tv_video = FindViewById<TextView> (Resource.Id.tv_video);
			v_indicator = FindViewById<View> (Resource.Id.v_indicator);
			vp = FindViewById<ViewPager> (Resource.Id.view_pager);
			ll_frame = FindViewById<LinearLayout> (Resource.Id.ll_frame);
		}

		public override void initListner ()
		{
			tv_video.SetOnClickListener (this);
			tv_audio.SetOnClickListener (this);
			vp.SetOnPageChangeListener (new MyPageChangeListener (this, this));
		}

		public class MyPageChangeListener : Java.Lang.Object, ViewPager.IOnPageChangeListener
		{
			Context _context;
			HomeActivity _hm;

			public MyPageChangeListener (Context context, HomeActivity home)
			{
				_context = context;	
				this._hm = home;
			}

			#region IOnPageChangeListener implementation

			//scroll state changed
			public void OnPageScrollStateChanged (int p0)
			{
				
			}
			//scrolling
			public void OnPageScrolled (int p0, float p1, int p2)
			{
				_hm.scrollIndicator (p0, p1);
			}

			public void OnPageSelected (int position)
			{
				_hm.changeTitleStatus (position == 0);
			}

			#endregion
		}

		private void scrollIndicator (int position, float positionOffset)
		{
			float translationX = width * position + width * positionOffset;
			ViewHelper.SetTranslationX (v_indicator, translationX);

		}

		private void changeTitleStatus (bool status)
		{
			tv_video.Selected = status;
			tv_audio.Selected = (!status);
			float scale = status ? 1.2f : 1.0f;
			float scaleau = status ? 1.0f : 1.2f;
			NineOldAndroids.View.ViewPropertyAnimator.Animate (tv_video).ScaleX (scale).ScaleY (scale);
			NineOldAndroids.View.ViewPropertyAnimator.Animate (tv_audio).ScaleX (scaleau).ScaleY (scaleau);
		}

		public override void initData ()
		{
			changeTitleStatus (true);
			initViewPager ();
			initIndicator ();
		}

		private void initIndicator ()
		{
			int winwidth = Utils.getWindowWidth (this);
			width = winwidth / pageSize;
			ViewGroup.LayoutParams lp = v_indicator.LayoutParameters;
			lp.Width = width;
			v_indicator.RequestLayout ();
		}

		private void initViewPager ()
		{
			lists = new List<Android.Support.V4.App.Fragment> ();
			lists.Add (new VideoListFragment ());
			lists.Add (new DownListFragment ());
			pageSize = lists.Count;
			MainAdapter ma = new MainAdapter (SupportFragmentManager, lists);
			vp.Adapter = ma;
		}

		public override int getLayoutResource ()
		{
			return Resource.Layout.activity_home;
		}

		public override void onClick (View v, int id)
		{
			switch (id) {
			case Resource.Id.tv_video:
				vp.SetCurrentItem (0, true);
				break;
			case Resource.Id.tv_downlessons:
				vp.SetCurrentItem (1, true);
				break;
			default:
				break;
			}
		}
	}
}
