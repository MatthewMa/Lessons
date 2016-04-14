using System;
using Android.OS;
using Android.Content;
using Android.App;

namespace LessonBasketDemo
{
	public class MyUIHandler:Handler
	{
		private Context context;

		public MyUIHandler (Context context)
		{
			this.context = context;
		}

		public override void HandleMessage (Message msg)
		{
			switch (msg.What) {
			case Constants.REQUEST_FAILED:
				DialogFactory.ToastDialog (context, "Server Error", "Server is busy,please try again later!", 1);
				break;
			case Constants.NET_ERROR:
				DialogFactory.ToastDialog (context, "Net Error", "Cannot connect to server,please check your internet!", 1);
				break;
			default:

				break;
			}
		}
	}
}

