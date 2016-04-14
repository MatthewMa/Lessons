using System;
using Android.Widget;
using Android.Content;
using Android.Database;
using Android.Views;
using Android.Text.Format;


namespace LessonBasketDemo
{
	public class VideoListAdapter: CursorAdapter
	{
		public VideoListAdapter (Context context, ICursor cursor) : base (context, cursor)
		{
			
		}

		/// <param name="context">Interface to application's global information</param>
		/// <param name="cursor">The cursor from which to get the data. The cursor is already
		///  moved to the correct position.</param>
		/// <param name="parent">The parent to which the new view is attached to</param>
		/// <summary>
		/// Makes a new view to hold the data pointed to by cursor.
		/// </summary>
		/// <returns>To be added.</returns>
		public override View NewView (Context context, ICursor cursor, ViewGroup parent)
		{
			//create a view
			View view = View.Inflate (context, Resource.Layout.adapter_videolist, null);

			//use viewholder
			ViewHolder vh = new ViewHolder ();
			vh.tv_title = view.FindViewById<TextView> (Resource.Id.tv_title);
			vh.tv_duration = view.FindViewById<TextView> (Resource.Id.tv_duration);
			vh.tv_size = view.FindViewById<TextView> (Resource.Id.tv_size);
			//save viewholder
			view.Tag = vh;
			return view;
		}

		public override void BindView (View view, Context context, ICursor cursor)
		{
			//extract Viewholder
			ViewHolder vh = (ViewHolder)view.Tag;
			VideoItem item = VideoItem.fromCursor (cursor);
			//display
			vh.tv_title.Text = item.Title;
			vh.tv_duration.Text = Utils.formatMillis (item.Duration);
			vh.tv_size.Text = Formatter.FormatFileSize (context, item.Size);
		}

		class ViewHolder:Java.Lang.Object
		{
			public TextView tv_title;
			public TextView tv_duration;
			public TextView tv_size;
		}
	}
}

