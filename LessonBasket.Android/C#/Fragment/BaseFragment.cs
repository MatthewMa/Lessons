using System;
using Android.Support.V4.App;
using Android.Content.PM;
using Android.Views;
using Android.OS;

namespace LessonBasketDemo
{
	public abstract class BaseFragment:Fragment,UiOperation
	{
		public View rootview;

		public override View OnCreateView (LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
		{
			rootview = inflater.Inflate (getLayoutResource (), null);
			Utils.findButtonSetOnClickListner (rootview, this);
			/*Button btn_back=findView(R.id.btn_back);*/
			initView ();//init view
			initListner ();//init listener
			initData ();//init data
			return rootview;
		}

		public abstract void initListner ();

		public abstract void initData ();

		public abstract void initView ();

		public abstract int getLayoutResource ();


		public abstract void onClick (View v, int id);

		public void OnClick (View v)
		{
			switch (v.Id) {
			case Resource.Id.btn_back:
				Activity.Finish (); //Common operations
				break;
			default:
				onClick (v, v.Id);
				break;
			}
		}
	}
}

