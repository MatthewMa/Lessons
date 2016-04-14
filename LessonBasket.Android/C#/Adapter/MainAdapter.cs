using System;
using Android.Support.V4.View;
using Android.Support.V4.App;
using System.Collections.Generic;

namespace LessonBasketDemo
{
	public class MainAdapter:FragmentPagerAdapter
	{
		private List<Fragment> fragments;

		public MainAdapter (FragmentManager fm, List<Fragment> frags) : base (fm)
		{
			this.fragments = frags;
		}


		public override long GetItemId (int position)
		{
			return position;
		}

		public override Android.Support.V4.App.Fragment GetItem (int position)
		{
			return fragments [position];
		}

		public override int Count {
			get { return fragments.Count; }
		}
	}
}

