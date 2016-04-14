
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Support.V4.App;

namespace LessonBasketDemo
{
	[Activity (Label = "BaseActivity")]			
	public abstract class BaseActivity : FragmentActivity,UiOperation
	{
		protected override void OnCreate (Bundle savedInstanceState)
		{
			base.OnCreate (savedInstanceState);
			RequestWindowFeature (WindowFeatures.NoTitle);
			SetContentView (getLayoutResource ());
			// Create your application here
			View root = FindViewById<View> (Android.Resource.Id.Content);
			Utils.findButtonSetOnClickListner (root, this);
			initView ();
			initListner ();
			initData ();

		}

		/// <summary>
		/// Initiate the listner
		/// </summary>
		public abstract  void initListner ();

		/// <summary>
		/// initiate data
		/// </summary>
		public abstract void initData ();

		/// <summary>
		/// Inits the view.
		/// </summary>
		public abstract void initView ();

		/// <summary>
		/// Gets the layout resource.
		/// </summary>
		/// <returns>The layout resource.</returns>
		public abstract int getLayoutResource ();

		public abstract void onClick (View v, int id);

		/// <summary>
		/// Ons the click.
		/// </summary>
		/// <param name="v">V.</param>
		/// <param name="id">Identifier.</param>
		public void OnClick (View v)
		{
			switch (v.Id) {
			case Resource.Id.btn_back:
				Finish (); //Common Operation
				break;
			default:
				onClick (v, v.Id);
				break;
			}
		}
	}
}

