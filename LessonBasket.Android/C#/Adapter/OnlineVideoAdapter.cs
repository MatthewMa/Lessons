using System;
using Android.Widget;
using System.Collections.Generic;
using Android.Views;
using Android.Content;

namespace LessonBasketDemo
{
	//not optimized
	public class OnlineVideoAdapter:BaseAdapter
	{
		private List<OnlineVideoItem> list;
		private Context context;
		private TextView tv_title;
		private TextView tv_size;
		private TextView tv_description;

		public OnlineVideoAdapter (List<OnlineVideoItem> list, Context context) : base ()
		{
			this.list = list;
			this.context = context;
		}

		public override Java.Lang.Object GetItem (int position)
		{
			return list [position];
		}

		public override long GetItemId (int position)
		{
			return position;
		}

		public override Android.Views.View GetView (int position, Android.Views.View convertView, Android.Views.ViewGroup parent)
		{
			View view = View.Inflate (context, Resource.Layout.adapter_onlinevideolist, null);
			tv_title = view.FindViewById<TextView> (Resource.Id.tv_title);
			tv_size = view.FindViewById<TextView> (Resource.Id.tv_size);
			tv_description = view.FindViewById<TextView> (Resource.Id.tv_description);
			tv_title.Text = list [position].Title;
			tv_size.Text = list [position].Size + " questions in total";
			tv_description.Text = list [position].Description;
			return view;
		}

		public override int Count {
			get {
				return list.Count;
			}
		}

	}
}

