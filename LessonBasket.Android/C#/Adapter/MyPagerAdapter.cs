using System;
using Android.Support.V4.View;
using Android.Views;
using System.Collections.Generic;
using Android.Widget;
using Android.Content;

namespace LessonBasketDemo
{
	public class MyPagerAdapter:PagerAdapter
	{
		private List<int> imagelist;

		public MyPagerAdapter (int[] images)
		{
			imagelist = new List<int> ();
			imagelist.AddRange (images);
		}

		public override bool IsViewFromObject (Android.Views.View view, Java.Lang.Object @object)
		{
			return @object == view;
		}

		//return the max count times
		public override int Count {
			get {
				return int.MaxValue;
			}
		}

		//remove view from container
		public override void DestroyItem (Android.Views.ViewGroup container, int position, Java.Lang.Object @object)
		{
			container.RemoveView ((View)@object);
		}

		/// <summary>
		/// Instantiates the item, create imageview
		/// </summary>
		/// <returns>The item.</returns>
		/// <param name="container">Container.</param>
		/// <param name="position">Position.</param>
		public override Java.Lang.Object InstantiateItem (Android.Views.ViewGroup container, int position)
		{
			ImageView iv = new ImageView (Android.App.Application.Context);
			position = position % imagelist.Count;
			iv.SetBackgroundResource (imagelist [position]);
			container.AddView (iv);
			return iv;
		}
	}
}

