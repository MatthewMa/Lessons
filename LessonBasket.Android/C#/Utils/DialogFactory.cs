using System;
using Android.Content;
using Android.App;
using Android.Views;
using Android.Widget;

namespace LessonBasketDemo
{
	public class DialogFactory
	{
		public static void ToastDialog (Context context, String title, String msg, int flag)
		{
			AlertDialog.Builder ab = new AlertDialog.Builder (context);
			ab.SetTitle (title);
			ab.SetMessage (msg);
			ab.SetPositiveButton ("confirm", delegate(object sender, DialogClickEventArgs e) {
				if (flag == 1) {//return to login activity
					(context as Activity).StartActivity (new Intent (context, typeof(LoginActivity)));
					(context as Activity).Finish ();
				} else if (flag == 2) {
					//return to question activity
					(context as VideoPlayerActivity).enterQuestion ();
				} else if (flag == 3) {
					//return to index activity
					context.StartActivity (new Intent (context, typeof(IndexActivity)));
					(context as HomeActivity).Finish ();
				} else if (flag == 4) {
					//return to home activity
					(context as VideoPlayerActivity).StartActivity (new Intent (context, typeof(HomeActivity)));
					(context as VideoPlayerActivity).Finish ();
				} else if (flag == 5) {
					(context as QuestionnairActivity).StartActivity (new Intent (context, typeof(HomeActivity)));
					(context as QuestionnairActivity).Finish ();
				} else if (flag == 6) {
					//todo: start result screen
				}
			});
			ab.Create ().Show ();
		}

		/// <summary>
		/// customed dialog
		/// </summary>
		/// <returns>The request dialog.</returns>
		/// <param name="">.</param>
		/// <param name="tip">Tip.</param>
		public static Dialog creatRequestDialog (Context context, String tip)
		{

			Dialog dialog = new Dialog (context, Resource.Style.dialog);
			dialog.SetContentView (Resource.Layout.dialog_layout);
			Window window = dialog.Window;
			WindowManagerLayoutParams lp = window.Attributes;
			int width = Utils.getWindowWidth (context);
			lp.Width = (int)(0.6 * width);

			TextView titleTxtv = dialog.FindViewById<TextView> (Resource.Id.tvLoad);
			if (tip == null || tip.Length == 0) {
				titleTxtv.Text = "Now sending request...";
			} else {
				titleTxtv.Text = tip;
			}

			return dialog;
		}

		public static void toastNegativePositiveDialog (Context context, String title, String msg, int flag)
		{
			AlertDialog.Builder ab = new AlertDialog.Builder (context);
			ab.SetTitle (title);
			ab.SetMessage (msg);
			ab.SetPositiveButton ("Confirm", delegate(object sender, DialogClickEventArgs e) {
				if (flag == 1) {//go to question activity
					(context as VideoPlayerActivity).enterQuestion ();
				} else if (flag == 2) {
					//todo:start the result activity

				} else if (flag == 3) {
					//log out
					context.StartActivity (new Intent (context, typeof(LoginActivity)));
					(context as IndexActivity).Finish ();
				} else if (flag == 4) {
					//return to videolist
					context.StartActivity (new Intent (context, typeof(HomeActivity)));
					(context as VideoPlayerActivity).Finish ();
				}
			});
			ab.SetNegativeButton ("Cancel", delegate(object sender, DialogClickEventArgs e) {
				
			});
			ab.Create ().Show ();
		}
	}
}

