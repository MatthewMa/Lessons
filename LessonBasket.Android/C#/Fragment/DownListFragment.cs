using System;
using Android.Views;
using Android.Widget;
using Android.Database;
using Android.Provider;
using Android.Content;
using System.Collections.Generic;
using Java.IO;

namespace LessonBasketDemo
{
	public class DownListFragment:BaseFragment,ISerializable
	{
		private ListView lv_downloaded;
		private bool isFromOnline;

		public override void initListner ()
		{
			lv_downloaded.ItemClick += delegate(object sender, AdapterView.ItemClickEventArgs e) {
				ICursor cursor = (ICursor)e.Parent.GetItemAtPosition (e.Position);
				List<VideoItem> videoItems = getVideoList (cursor);
				isFromOnline = false;
				enterVideoPlayActivity (videoItems, e.Position);

			};
		}

		/// <summary>
		/// Gets the video list.
		/// </summary>
		/// <returns>The video list.</returns>
		/// <param name="cursor">Cursor.</param>
		List<VideoItem> getVideoList (ICursor cursor)
		{
			List<VideoItem> videoItems = null;
			if (cursor != null) {
				videoItems = new List<VideoItem> ();
				cursor.MoveToFirst ();
				do {
					videoItems.Add (VideoItem.fromCursor (cursor));
				} while(cursor.MoveToNext ());
			}
			return videoItems;
		}

		private void enterVideoPlayActivity (IList<VideoItem> lists, int position)
		{
			Intent intent = new Intent (this.Activity, typeof(VideoPlayerActivity));
			string json = Utils.ToJSON (lists);
			intent.PutExtra (Keys.ITEM_LIST, json);
			if (!string.IsNullOrEmpty (json))
				System.Console.WriteLine ();
			intent.PutExtra (Keys.CURRENT_POSITION, position);
			intent.PutExtra (Keys.ISFROMONLINE, isFromOnline);
			StartActivity (intent);
		}

		public override void initData ()
		{
			//load local file
			MyAsync my = new MyAsync (this.Activity.ContentResolver, lv_downloaded, this.Activity);
			int token = 0;
			Android.Net.Uri uri = MediaStore.Video.Media.ExternalContentUri;
			String[] projection = {
				MediaStore.Video.Media.InterfaceConsts.Id + " as _id",
				MediaStore.Video.Media.InterfaceConsts.Title,
				MediaStore.Video.Media.InterfaceConsts.Duration,
				MediaStore.Video.Media.InterfaceConsts.Size,
				MediaStore.Video.Media.InterfaceConsts.Data
			};
			String orderby = MediaStore.Video.Media.InterfaceConsts.Title + " ASC";
			my.StartQuery (token, null, uri, projection, null, null, orderby);
		}


		public override void initView ()
		{
			lv_downloaded = rootview.FindViewById<ListView> (Resource.Id.lv_downloaded);
		}


		public override int getLayoutResource ()
		{
			return Resource.Layout.fragment_downvideolist;
		}


		public override void onClick (View v, int id)
		{

		}

	}

	public class MyAsync:AsyncQueryHandler
	{
		private ListView lv;
		private Context context;

		public MyAsync (ContentResolver r, ListView v, Context c) : base (r)
		{
			this.lv = v;
			this.context = c;
		}

		protected override void OnQueryComplete (int token, Java.Lang.Object cookie, ICursor cursor)
		{
			VideoListAdapter va = new VideoListAdapter (context, cursor);
			lv.Adapter = va;
		}
	}
}

