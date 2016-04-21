using System;
using Android.Widget;
using System.Collections.Generic;
using Android.Views;
using Android.Content;

namespace LessonBasketDemo
{
	public class OnlineVideoAdapter:BaseAdapter
	{
		private List<OnlineVideoItem> list;
		private Context context;


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
			ViewHolder vh = new ViewHolder ();
			if (convertView == null) {
				View view = View.Inflate (context, Resource.Layout.adapter_onlinevideolist, null);
				vh.tv_title = view.FindViewById<TextView> (Resource.Id.tv_title);
				vh.tv_size = view.FindViewById<TextView> (Resource.Id.tv_size);
				vh.tv_description = view.FindViewById<TextView> (Resource.Id.tv_description);
				convertView = view;
				convertView.Tag = vh;
			} else {
				vh = (ViewHolder)convertView.Tag;
			}
			vh.tv_title.Text = list [position].Title;
			vh.tv_size.Text = list [position].Size + " questions in total";
			vh.tv_description.Text = list [position].Description;
			return convertView;
		}

		public override int Count {
			get {
				return list.Count;
			}
		}
	}

	class ViewHolder:Java.Lang.Object
	{
		public  TextView tv_title;
		public  TextView tv_size;
		public  TextView tv_description;
	}
}

