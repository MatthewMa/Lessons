
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
using Android.Text;
using System.Text.RegularExpressions;
using Android.Preferences;
using Android.App.Admin;

namespace LessonBasketDemo
{
	[Activity (Label = "LessonBasketDemo", ScreenOrientation = Android.Content.PM.ScreenOrientation.Portrait)]			
	public class LoginActivity : BaseActivity
	{
		private Button login_btn;
		private Button btn_cancel;
		private Button btn_reset;
		private CheckBox auto_save_password;
		private Button regist_btn;
		private EditText login_accounts;
		private EditText login_password;
		private string email;
		private string password;
		private ISharedPreferences sp;
		private ISharedPreferencesEditor edit;
		private View popView;
		//popView
		private EditText et_email;
		private Button btn_send;
		private Button btn_resetcancel;

		#region implemented abstract members of BaseActivity

		public override void initListner ()
		{
			
		}

		public override void initData ()
		{
			auto_save_password.Checked = sp.GetBoolean ("checked", true);
			//remember me?
			if (auto_save_password.Checked) {
				login_accounts.Text = sp.GetString ("email", "");
				login_password.Text = sp.GetString ("password", "");
			}
		}

		public override void initView ()
		{
			login_btn = FindViewById<Button> (Resource.Id.login_btn);
			btn_cancel = FindViewById<Button> (Resource.Id.btn_cancel);
			btn_reset = FindViewById<Button> (Resource.Id.btn_reset);
			auto_save_password = FindViewById<CheckBox> (Resource.Id.auto_save_password);
			regist_btn = FindViewById<Button> (Resource.Id.regist_btn);
			login_accounts = FindViewById<EditText> (Resource.Id.login_accounts);
			login_password = FindViewById<EditText> (Resource.Id.login_password);
			sp = PreferenceManager.GetDefaultSharedPreferences (this);
			edit = sp.Edit ();
		}

		public override int getLayoutResource ()
		{
			return Resource.Layout.activity_login;
		}

		public override void onClick (View v, int id)
		{
			switch (id) {
			case Resource.Id.login_btn://login 
				Login ();
				break;
			case Resource.Id.btn_cancel://cancel
				Finish ();
				break;
			case Resource.Id.btn_reset:
				ResetPassword ();
				break;
			case Resource.Id.regist_btn:
				Register ();
				break;
			default:
				break;
			}
		}

		#endregion

		/// <summary>
		/// Login this instance.
		/// </summary>
		public void Login ()
		{
			email = login_accounts.Text;
			password = login_password.Text;
			//check empty
			if (string.IsNullOrEmpty (email) || string.IsNullOrEmpty (password)) {
				DialogFactory.ToastDialog (this, "Login", "Username and password cannot be empty!", 0);
			} else {
				//check email
				Match match = Regex.Match (email, "^(\\w)+(\\.\\w+)*@(\\w)+((\\.\\w{2,3}){1,3})$");
				if (match.Success) {
					//check email and password to server

					//login successfully
					//remember me?
					bool check = auto_save_password.Checked;
					if (auto_save_password.Checked) {
						edit.PutString ("email", email);
						edit.PutString ("password", password);
						edit.PutBoolean ("checked", check);
						edit.Commit ();
					} else {
						edit.PutBoolean ("checked", check);
						edit.Commit ();
					}
					Intent intent = new Intent (this, typeof(SplashActivity));
					intent.PutExtra ("email", email);
					StartActivity (intent);
					Finish ();
				} else {
					DialogFactory.ToastDialog (this, "Login", "Email format is incorrent!", 0);
				}
			}
		}

		/// <summary>
		/// Register an account
		/// </summary>
		private void Register ()
		{
			StartActivity (new Intent (this, typeof(RegisterActivity)));
			Finish ();
		}

		private void ResetPassword ()
		{
			popView = View.Inflate (this, Resource.Layout.dialog_resetpassword, null);
			et_email = popView.FindViewById<EditText> (Resource.Id.et_emailaddress);
			btn_send = popView.FindViewById<Button> (Resource.Id.btn_send);
			btn_resetcancel = popView.FindViewById<Button> (Resource.Id.btn_cancel);
			PopupWindow pop = new PopupWindow (popView, 400, 400);
			pop.ShowAtLocation (btn_reset, GravityFlags.Center, 0, -100);
			//set click listener
			btn_send.Click += delegate(object sender, EventArgs e) {
				//check email empty
				string address = et_email.Text;
				if (string.IsNullOrEmpty (address)) {
					DialogFactory.ToastDialog (this, "Reset Password", "Email address cannot be empty!", 0);
				} else {
					//todo:send an email
				}
			};
			btn_resetcancel.Click += delegate(object sender, EventArgs e) {
				pop.Dismiss ();	
			};
		}
	}
}

