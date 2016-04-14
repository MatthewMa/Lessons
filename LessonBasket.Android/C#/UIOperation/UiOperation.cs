using System;
using Android.Views;

namespace LessonBasketDemo
{
	public interface UiOperation:View.IOnClickListener
	{
		/// <summary>
		/// Initiate the listner
		/// </summary>
		void initListner();

		/// <summary>
		/// initiate data
		/// </summary>
		void initData();

		/// <summary>
		/// Inits the view.
		/// </summary>
		void initView();

		/// <summary>
		/// Gets the layout resource.
		/// </summary>
		/// <returns>The layout resource.</returns>
		int getLayoutResource();

		/// <summary>
		/// Ons the click.
		/// </summary>
		/// <param name="v">V.</param>
		/// <param name="id">Identifier.</param>
		void onClick(View v,int id);
	}
}

