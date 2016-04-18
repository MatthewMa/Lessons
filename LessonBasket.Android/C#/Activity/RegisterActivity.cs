
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

namespace LessonBasketDemo
{
	[Activity (Label = "LessonBasketDemo", ScreenOrientation = Android.Content.PM.ScreenOrientation.Portrait)]				
	public class RegisterActivity : BaseActivity
	{
		public override void initListner ()
		{
			
		}

		public override void initData ()
		{
			
		}

		public override void initView ()
		{
			
		}

		public override int getLayoutResource ()
		{
			return Resource.Layout.activity_register;
		}

		public override void onClick (View v, int id)
		{
			switch (id) {
			case Resource.Id.btn_regback:
				StartActivity (new Intent (this, typeof(LoginActivity)));
				Finish ();
				break;
			case Resource.Id.btn_continue:
				CompleteRegister ();
				break;
			default:
				break;
			}
		}

		public override void OnBackPressed ()
		{
		}

		/// <summary>
		/// Completes the register.
		/// </summary>
		private void CompleteRegister ()
		{
			
		}
	}
}

