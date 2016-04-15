using System;
using Android.Views;
using Android.Widget;
using Android.Content;
using Android.Provider;
using Android.Database;
using System.Collections.Generic;
using System.ComponentModel;
using Android.OS;
using Java.IO;
using Android.App;
using Org.Apache.Http.Cookies;
using System.Text;
using LessonBasket;

namespace LessonBasketDemo
{
	public class VideoListFragment:BaseFragment,ISerializable
	{
		private List<OnlineVideoItem> list;
		private ListView lv_online;
		private bool isFromOnline;
		private Lesson lesson;

		public override void initListner ()
		{
			lv_online.ItemClick += delegate(object sender, AdapterView.ItemClickEventArgs e) {
				isFromOnline = true;
				enterOnlineVideoPlayActivity (e.Position);
			};
		}



		/// <summary>
		/// 网络视频进入
		/// </summary>
		/// <param name="position">Position.</param>
		private void enterOnlineVideoPlayActivity (int position)
		{
			Intent intent = new Intent (this.Activity, typeof(VideoPlayerActivity));
			string json = Utils.ToJSON (list);
			intent.PutExtra (Keys.ITEM_LIST, json);
			intent.PutExtra (Keys.CURRENT_POSITION, position);
			intent.PutExtra (Keys.ISFROMONLINE, isFromOnline);
			StartActivity (intent);
		}

		/// <summary>
		/// put cursor's records to a list
		/// </summary>
		/// <returns>The video list.</returns>
		/// <param name="cursor">Cursor.</param>


		public override void initData ()
		{
			//load online from local database
			list = new List<OnlineVideoItem> ();
			int count = Constants.lessons_url.Count;//count video urls
			if (count != 0) {
				for (int i = 0; i < count; i++) {
					//query and store info about lessons
					try {
						lesson = LessonUtil.getLessonFromRest (Constants.lessons_url [i]).Result;
					} catch (Exception ex) {
						DialogFactory.ToastDialog (this.Activity, "Data Error", "Data Error, please try again!", 3);
					}
					OnlineVideoItem it = new OnlineVideoItem ();
					it.Description = lesson.description;
					it.Title = lesson.title;
					it.Size = lesson.screenCount - 1;
					it.Url = new List<string> (lesson.screenList);
					list.Add (it);
				}
			}
			OnlineVideoAdapter online = new OnlineVideoAdapter (list, this.Activity);
			lv_online.Adapter = online;
		}


		public override void initView ()
		{
			lv_online = rootview.FindViewById<ListView> (Resource.Id.lv_online);
		}


		public override int getLayoutResource ()
		{
			return Resource.Layout.fragment_videolist;
		}


		public override void onClick (View v, int id)
		{

		}

	}
		

}

