using System;
using Android.Database;
using Android.Provider;

namespace LessonBasketDemo
{
	[Serializable]
	public class VideoItem
	{
		private String title;
		private long duration;
		private long size;
		private String path;

		public string Title {
			get {
				return this.title;
			}
			set {
				title = value;
			}
		}

		public long Duration {
			get {
				return this.duration;
			}
			set {
				duration = value;
			}
		}

		public long Size {
			get {
				return this.size;
			}
			set {
				size = value;
			}
		}

		public string Path {
			get {
				return this.path;
			}
			set {
				path = value;
			}
		}

		public static VideoItem fromCursor (ICursor cursor)
		{
			VideoItem item = new VideoItem ();
			item.Title = cursor.GetString (1);
			item.Duration = cursor.GetLong (2);
			item.Size = cursor.GetLong (3);
			item.Path = cursor.GetString (cursor.GetColumnIndex (MediaStore.Video.Media.InterfaceConsts.Data));
			return item;
		}


		public VideoItem ()
		{
		}
	}
}

