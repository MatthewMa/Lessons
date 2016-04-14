using System;
using Android.Widget;
using System.Collections.Generic;
using Android.App;
using Android.Views;
using Android.Content;

namespace LessonBasketDemo
{
	public class MyGridViewAdapter:BaseAdapter
	{
		private List<string> title;
		private List<int> resources;
		private Context context;
		private TextView tv_item;
		private ImageView iv_item;

		public override Java.Lang.Object GetItem (int position)
		{
			return title [position];
		}

		public override long GetItemId (int position)
		{
			return position;
		}

		public override Android.Views.View GetView (int position, Android.Views.View convertView, Android.Views.ViewGroup parent)
		{
			View view = View.Inflate (context, Resource.Layout.gridview_listitem, null);
			tv_item = view.FindViewById<TextView> (Resource.Id.tv_item);
			iv_item = view.FindViewById<ImageView> (Resource.Id.iv_item);
			tv_item.Text = title [position];
			iv_item.SetImageResource (resources [position]);
			return view;
		}

		public override int Count {
			get {
				return title.Count;
			}
		}

		public MyGridViewAdapter (string[] title, int[] resources, Context context)
		{
			this.title = new List<string> ();
			this.resources = new List<int> ();
			this.title.AddRange (title);
			this.resources.AddRange (resources);
			this.context = context;
		}
	}
}

