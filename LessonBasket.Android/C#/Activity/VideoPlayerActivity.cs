﻿
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
using System.Security.Cryptography;
using Android.Media;
using NineOldAndroids.View;
using IO.Vov.Vitamio;
using Android.Text.Format;
using Javax.Crypto.Interfaces;
using Android.Gestures;
using Android.Support.V4.View;
using Java.Security;
using NineOldAndroids.Animation;
using Android.Content.PM;
using LessonBasket;

namespace LessonBasketDemo
{
	[Activity (Label = "VideoPlay", ScreenOrientation = Android.Content.PM.ScreenOrientation.Portrait, Theme = "@android:style/Theme.Black.NoTitleBar.Fullscreen",
		ConfigurationChanges = Android.Content.PM.ConfigChanges.Orientation | Android.Content.PM.ConfigChanges.ScreenSize)]			
	public class VideoPlayerActivity : BaseActivity
	{
		private int currentPosition;
		private LinearLayout ll_top_ctrl;
		private LinearLayout ll_bottom_ctrl;
		private TextView tv_title;
		private TextView tv_system_time;
		private TextView tv_current_position;
		private TextView tv_duration;
		private ImageView iv_battery;
		private SeekBar sb_voice;
		private SeekBar sb_video;
		private Button btn_pre;
		private Button btn_play;
		private Button btn_next;
		private Button btn_fullscreen;
		private Button btn_voice;
		private Button btn_startquestion;
		private GestureDetector gestureDetector;
		private MyGestureListener mygesture;
		private float brightness;
		private List<VideoItem> videoItems;
		private List<OnlineVideoItem> onlinevideoItems;
		private VideoItem currentVideoItem;
		private OnlineVideoItem currentOnlineVideoItem;
		private IO.Vov.Vitamio.Widget.VideoView vv;
		private MyHandler handler;
		//Braightness and volume
		private AudioManager audioManager;
		private int maxVolume;
		public int currentVolume;
		private float maxVlumeScreenHeightScale;
		private float maxBrightnessScreenHeightScale;
		private View view_brightness;
		private bool isFromOnline;
		private LinearLayout ll_loading;
		//full screen->not full
		private bool isFullScreen;
		private int vvHeight;
		private int vvWidth;
		private int screenWidth;
		private int screenHeight;
		//Broadcastreceiver
		private BatteryChangedReceiver batteryChangedReceiver;

		//Comment
		private LinearLayout ll_comment;
		private Screen screen;

		public override void initListner ()
		{
			sb_voice.ProgressChanged += ((sender, e) => {
				if (e.FromUser) {
					setStreamVolume (e.Progress);
				}
			});
			sb_video.ProgressChanged += delegate(object sender, SeekBar.ProgressChangedEventArgs e) {
				if (e.FromUser) {
					if (vv.IsPlaying) {
						vv.SeekTo (e.Progress);
					} else {
						vv.SeekTo (e.Progress);
						btn_play.SetBackgroundResource (Resource.Drawable.selector_btn_pause);
					}
				}
			};
			//seek bar progress remove the hide message 
			sb_voice.StartTrackingTouch += delegate(object sender, SeekBar.StartTrackingTouchEventArgs e) {
				removeCtlHideMessage ();
			};
			sb_voice.StopTrackingTouch += delegate(object sender, SeekBar.StopTrackingTouchEventArgs e) {
				sendCtlHideMessage ();
			};
			sb_video.StartTrackingTouch += delegate(object sender, SeekBar.StartTrackingTouchEventArgs e) {
				removeCtlHideMessage ();	
			};
			sb_video.StopTrackingTouch += delegate(object sender, SeekBar.StopTrackingTouchEventArgs e) {
				sendCtlHideMessage ();
			};

			mygesture = new MyGestureListener (this, view_brightness);
			gestureDetector = new GestureDetector (mygesture);
			//video buffer listener
			vv.BufferingUpdate += delegate(object sender, IO.Vov.Vitamio.MediaPlayer.BufferingUpdateEventArgs e) {
				updateSecondaryProgress (e);
			};
			//set buffered slow listener
			vv.Info += delegate(object sender, IO.Vov.Vitamio.MediaPlayer.InfoEventArgs e) {
				switch (e.P1) {
				//buffer started
				case (int)MediaInfo.BufferingStart:
					ll_loading.Visibility = ViewStates.Visible;
					break;
				//buffer end then play
				case (int)MediaInfo.BufferingEnd:
					hideLoadingCtl ();
					break;
				default:
					break;
				}
			};
			//start question
			btn_startquestion.Click += delegate(object sender, EventArgs e) {
				DialogFactory.toastNegativePositiveDialog (this, "Answer question now", "Are you sure to start answering questions now?", 1);
			};

		}

		/// <summary>
		/// Updates the secondary progress.
		/// </summary>
		/// <param name="e">E.</param>
		private void updateSecondaryProgress (IO.Vov.Vitamio.MediaPlayer.BufferingUpdateEventArgs e)
		{
			float percentFloat = e.P1 / 100.0f;
			int secondaryProgress = (int)(vv.Duration * percentFloat);
			sb_video.SecondaryProgress = secondaryProgress;
		}

		/// <summary>
		/// 设置音量
		/// </summary>
		/// <param name="index">Index.</param>
		private void setStreamVolume (int index)
		{
			if (index == 0) {
				btn_voice.SetBackgroundResource (Resource.Drawable.voice_mute);
			} else {
				btn_voice.SetBackgroundResource (Resource.Drawable.btn_voice_normal);
			}
			audioManager.SetStreamVolume (Stream.Music, index, VolumeNotificationFlags.PlaySound);
		}

		public override async void initData ()
		{
			
			//register battery change broadcast
			RegisterBatteryChangedReceiver ();
			isFromOnline = this.Intent.GetBooleanExtra (Keys.ISFROMONLINE, true);
			currentPosition = this.Intent.GetIntExtra (Keys.CURRENT_POSITION, -1);
			//check vitamio installed?
			if (!LibsChecker.CheckVitamioLibs (this)) {
				return;
			}
			if (isFromOnline) {
				//net resource
				string json = this.Intent.GetStringExtra (Keys.ITEM_LIST);
				if (!string.IsNullOrEmpty (json)) {
					onlinevideoItems = new List<OnlineVideoItem> ();
					onlinevideoItems = Utils.FromJSON<List<OnlineVideoItem>> (json);
					if (onlinevideoItems == null || onlinevideoItems.Count == 0 || currentPosition == -1) {
						return;
					}
					currentOnlineVideoItem = new OnlineVideoItem ();
					currentOnlineVideoItem = onlinevideoItems [currentPosition];
					//query
					try {
						screen = await LessonUtil.GetScreenByPositionAsync (currentPosition + 1, 0);
					} catch (Exception ex) {
						DialogFactory.ToastDialog (this, "Data Error", "Data error,please try again later!", 4);
					}
					if (screen != null) {
						vv.SetVideoURI (Android.Net.Uri.Parse (Utils.EncodeURL (screen.VideoUrl)));
					}

				}
			} else {
				//local resource
				string json = this.Intent.GetStringExtra (Keys.ITEM_LIST);
				if (!string.IsNullOrEmpty (json)) {
					videoItems = Utils.FromJSON<List<VideoItem>> (json);
					if (videoItems == null || videoItems.Count == 0 || currentPosition == -1) {
						return;
					}
					currentVideoItem = videoItems [currentPosition];
					vv.SetVideoPath (currentVideoItem.Path);

				}
				isFullScreen = false;
				updateFullScreen ();//update the full screen button
			}

			vv.RequestFocus ();
			vv.Prepared += delegate(object sender, IO.Vov.Vitamio.MediaPlayer.PreparedEventArgs e) {
				e.P0.SetPlaybackSpeed (1.0f);//start play
				vv.Start ();
				//put duration
				string duration = Utils.formatMillis (vv.Duration);//set duration
				tv_duration.Text = duration;
				sb_video.Max = (int)vv.Duration;
				updateCurrentPosition ();//update current position
				btn_play.SetBackgroundResource (Resource.Drawable.selector_btn_pause);//set play button
				if (isFromOnline) {
					tv_title.Text = currentOnlineVideoItem.Title + ":" + currentOnlineVideoItem.Description;//set video title
				} else {
					tv_title.Text = currentVideoItem.Title;//set video title
				}
				//check if the previous or next button enabled
				if (isFromOnline) {				
					btn_next.Enabled = (currentPosition != onlinevideoItems.Count - 1);
				} else {
					btn_next.Enabled = (currentPosition != videoItems.Count - 1);
				}
				btn_pre.Enabled = (currentPosition != 0);
				//hide the loading progress bar
				hideLoadingCtl ();
			};

			//video complete
			vv.Completion += delegate(object sender, IO.Vov.Vitamio.MediaPlayer.CompletionEventArgs e) {
				vv.SeekTo (0);
				tv_current_position.Text = Utils.formatMillis (0L);
				if (isFromOnline) {
					//if it is from online, then start the questionnair
					DialogFactory.ToastDialog (this, "Answer question", "Now let us start answering questions", 2);
				}

			};
			//control volume
			initVolume ();

			// scale volume
			maxVlumeScreenHeightScale = ((float)maxVolume) / Utils.getWindowWidth (this);

			// brightness scale
			maxBrightnessScreenHeightScale = 1.0f / Utils.getWindowHeight (this);
			//set screen width and height
			screenWidth = Utils.getWindowWidth (this);
			screenHeight = Utils.getWindowHeight (this);

		}

		/// <summary>
		/// Enters the question activity
		/// .
		/// </summary>
		public void enterQuestion ()
		{
			Intent intent = new Intent (this, typeof(QuestionnairActivity));
			intent.PutExtra ("index", currentPosition);
			StartActivity (intent);
			Finish ();
		}

		/// <summary>
		/// Hides the loading ctl.
		/// </summary>
		private void hideLoadingCtl ()
		{		
			//set end animationlistner	
			NineOldAndroids.View.ViewPropertyAnimator.Animate (ll_loading).Alpha (0.0f).SetDuration (1500).SetListener (new MyAnimatorListener (ll_loading));
		}

		public void updateCurrentPosition ()
		{
			int cp = (int)vv.CurrentPosition;
			tv_current_position.Text = Utils.formatMillis (cp);
			sb_video.Progress = cp;
			handler.SendEmptyMessageDelayed (Constants.UPDATE_CURRENT_POSITION, 300);
		}

		/// <summary>
		/// Inits the volume.
		/// </summary>
		void initVolume ()
		{
			audioManager = (AudioManager)GetSystemService (Context.AudioService);
			maxVolume = audioManager.GetStreamMaxVolume (Android.Media.Stream.Music);
			currentVolume = GetStreamVolume ();
			sb_voice.Max = maxVolume;
			sb_voice.Progress = currentVolume;
		}

		public int GetStreamVolume ()
		{
			return audioManager.GetStreamVolume (Stream.Music);
		}

		/// <summary>
		/// 界面可见
		/// </summary>
		protected override void OnStart ()
		{
			base.OnStart ();
			UpdateSystemTime ();
		}

		protected override void OnStop ()
		{
			base.OnStop ();
			handler.RemoveMessages (Constants.UPDATE_SYSTEM_TIME);
		}

		/// <summary>
		/// update the system time
		/// </summary>
		void UpdateSystemTime ()
		{
			tv_system_time.Text = DateFormat.Format ("kk:mm:ss", System.DateTime.Now.Millisecond);
			handler.SendEmptyMessageDelayed (Constants.UPDATE_SYSTEM_TIME, 1000);
		}

		/// <summary>
		/// Registers the battery changed receiver.
		/// </summary>
		public void RegisterBatteryChangedReceiver ()
		{
			IntentFilter filter = new IntentFilter (Intent.ActionBatteryChanged);
			batteryChangedReceiver = new BatteryChangedReceiver (iv_battery);
			RegisterReceiver (batteryChangedReceiver, filter);

		}

		protected override void OnDestroy ()
		{
			base.OnDestroy ();
			if (batteryChangedReceiver != null) {
				UnregisterReceiver (batteryChangedReceiver);
			}
			handler.RemoveCallbacksAndMessages (null);
		}

		public override void initView ()
		{
			vv = FindViewById<IO.Vov.Vitamio.Widget.VideoView> (Resource.Id.video_view);
			ll_top_ctrl = FindViewById<LinearLayout> (Resource.Id.ll_top_ctrl);
			ll_bottom_ctrl = FindViewById<LinearLayout> (Resource.Id.ll_bottom_ctrl);

			//TextView control
			tv_title = FindViewById<TextView> (Resource.Id.tv_title);
			tv_system_time = FindViewById<TextView> (Resource.Id.tv_system_time);
			tv_current_position = FindViewById<TextView> (Resource.Id.tv_current_position);
			tv_duration = FindViewById<TextView> (Resource.Id.tv_duration);

			iv_battery = FindViewById<ImageView> (Resource.Id.iv_battery);

			//seekbar
			sb_voice = FindViewById<SeekBar> (Resource.Id.sb_voice);
			sb_video = FindViewById<SeekBar> (Resource.Id.sb_video);

			//button
			btn_pre = FindViewById<Button> (Resource.Id.btn_pre);
			btn_play = FindViewById<Button> (Resource.Id.btn_play);
			btn_next = FindViewById<Button> (Resource.Id.btn_next);
			btn_fullscreen = FindViewById<Button> (Resource.Id.btn_fullscreen);
			btn_voice = FindViewById<Button> (Resource.Id.btn_voice);
			btn_startquestion = FindViewById<Button> (Resource.Id.btn_startquestion);

			view_brightness = FindViewById<View> (Resource.Id.view_brightness);
			view_brightness.Visibility = ViewStates.Visible;
			ViewHelper.SetAlpha (view_brightness, 0.0f);
			ll_loading = FindViewById<LinearLayout> (Resource.Id.ll_loading);
			ll_loading.Visibility = ViewStates.Visible;
			ll_comment = FindViewById<LinearLayout> (Resource.Id.ll_comment);
			handler = new MyHandler (tv_system_time, tv_current_position, vv, sb_video, this);
			initCtrlLayout ();
		}

		public override int getLayoutResource ()
		{
			return Resource.Layout.activity_videoplay;
		}

		public override void onClick (View v, int id)
		{
			removeCtlHideMessage ();
			switch (id) {
			case Resource.Id.btn_voice:// mute button
				toggleMute ();
				break;
			case Resource.Id.btn_exit:	// exit button
				DialogFactory.toastNegativePositiveDialog (this, "Return", "Are you sure to return to Lesson Lists?", 4);
				break;
			case Resource.Id.btn_pre:	// previous button
				pre ();
				break;
			case Resource.Id.btn_play:	// play/pause button
				play ();
				break;
			case Resource.Id.btn_next:	// next button
				next ();
				break;
			case Resource.Id.btn_fullscreen:	// full screen button
				toggleFullscreen ();
				updateFullScreen ();
				break;
			case Resource.Id.btn_startquestion://begin answering question
				if (isFromOnline) {
					//if it is from online, then start the questionnair
					DialogFactory.toastNegativePositiveDialog (this, "Answer question", "Are you sure to start answering questions now?", 1);
				}
				break;
			default:
				break;
			}
			sendCtlHideMessage ();
		}

		public async void openVideo ()
		{
			ll_loading.Visibility = ViewStates.Visible;
			if (isFromOnline) {				
				if (onlinevideoItems == null || onlinevideoItems.Count == 0 || currentPosition == -1)
					return;
				currentOnlineVideoItem = onlinevideoItems [currentPosition];
				string screen_url = currentOnlineVideoItem.Url [0];
				//go to server 

				try {
					screen = await LessonUtil.GetScreenByPositionAsync (currentPosition + 1, 0);
				} catch (Exception ex) {
					DialogFactory.ToastDialog (this, "Data Error", "Data error, please try again!", 4);
				}
				vv.SetVideoURI (Android.Net.Uri.Parse (Utils.EncodeURL (screen.VideoUrl)));
				btn_next.Enabled = (currentPosition != onlinevideoItems.Count - 1);
			} else {
				if (videoItems == null || videoItems.Count == 0 || currentPosition == -1) {
					return;
				}
				currentVideoItem = videoItems [currentPosition];
				vv.SetVideoPath (currentVideoItem.Path);
				btn_next.Enabled = (currentPosition != videoItems.Count - 1);
			}
			btn_pre.Enabled = (currentPosition != 0);
		}

		/// <summary>
		/// initialize linearlayout
		/// </summary>
		private void initCtrlLayout ()
		{
			//hide the control->setTranslate
			ll_top_ctrl.Measure (0, 0);//let the system do measure
			float translationY = ll_top_ctrl.MeasuredHeight;//get height
			ViewHelper.SetTranslationY (ll_top_ctrl, -translationY);
			//hide bottom contrl
			ll_bottom_ctrl.Measure (0, 0);
			float translationBY = ll_bottom_ctrl.MeasuredHeight;
			ViewHelper.SetTranslationY (ll_bottom_ctrl, translationBY);

		}

		/// <summary>
		/// Toggles the mute.
		/// </summary>
		protected void toggleMute ()
		{
			if (GetStreamVolume () > 0) {
				currentVolume = GetStreamVolume ();
				setStreamVolume (0);
				sb_voice.Progress = 0;
				btn_voice.SetBackgroundResource (Resource.Drawable.voice_mute);

			} else {
				setStreamVolume (currentVolume);
				sb_voice.Progress = currentVolume;
				btn_voice.SetBackgroundResource (Resource.Drawable.btn_voice_normal);
			}
		}

		/// <summary>
		/// previous button
		/// </summary>
		protected void pre ()
		{
			if (currentPosition > 0) {
				currentPosition--;
				openVideo ();
			}
		}

		/// <summary>
		/// play button
		/// </summary>
		public void play ()
		{
			if (vv.IsPlaying) {
				vv.Pause ();
			} else {
				vv.Start ();
			}
			updatePlayBtn ();
		}

		//更新播放按钮背景
		protected void updatePlayBtn ()
		{
			if (vv.IsPlaying) {
				btn_play.SetBackgroundResource (Resource.Drawable.selector_btn_pause);
			} else {
				btn_play.SetBackgroundResource (Resource.Drawable.selector_btn_play);
			}
		}

		/// <summary>
		/// next button
		/// </summary>
		protected void next ()
		{
			if (isFromOnline) {
				if (currentPosition < onlinevideoItems.Count - 1) {
					currentPosition++;
					openVideo ();
				}
			} else {
				if (currentPosition < videoItems.Count - 1) {
					currentPosition++;
					openVideo ();
				}
			}
		}

		/// <summary>
		/// full screen button
		/// </summary>
		public void toggleFullscreen ()
		{
			var p = (RelativeLayout.LayoutParams)vv.LayoutParameters;
			//set default width and height of videoview
			if (!isFullScreen) {
				vvWidth = vv.MeasuredWidth;
				vvHeight = vv.MeasuredHeight;
			}

			if (isFullScreen) {
				//current is full screen,switch to default
				p.Width = vvWidth;
				p.Height = vvHeight;
				this.RequestedOrientation = Android.Content.PM.ScreenOrientation.Portrait;
			} else {
				//current is default screen,switch to full screen
				p.Width = screenWidth;
				p.Height = screenHeight;
				this.RequestedOrientation = Android.Content.PM.ScreenOrientation.Landscape;
			}
			isFullScreen = !isFullScreen;
			vv.RequestLayout ();
		}

		/// <summary>
		/// Updates the full screen button.
		/// </summary>
		void updateFullScreen ()
		{
			int resourceid;
			if (isFullScreen) {
				resourceid = Resource.Drawable.selector_btn_fullscreen;
			} else {
				resourceid = Resource.Drawable.selector_btn_defaultscreen;
			}
			btn_fullscreen.SetBackgroundResource (resourceid);
		}

		/// <param name="e">The touch screen event being processed.</param>
		/// <summary>
		/// Called when a touch screen event was not handled by any of the views
		///  under it.
		/// </summary>
		/// <returns>To be added.</returns>
		public override bool OnTouchEvent (MotionEvent e)
		{
			gestureDetector.OnTouchEvent (e);
			switch (e.Action) {
			case MotionEventActions.Down:
				removeCtlHideMessage ();
				break;
			case MotionEventActions.Up:
				sendCtlHideMessage ();
				break;
			default:
				break;

			}
			return base.OnTouchEvent (e);

		}

		/** change volume */
		public void changeVolume (float distanceYY)
		{
			int moveVolume = (int)(distanceYY * maxVlumeScreenHeightScale);
			int result = currentVolume + moveVolume;

			// extends 
			if (result > maxVolume) {
				result = maxVolume;
			} else if (result < 0) {
				result = 0;
			}
			setStreamVolume (result);
			sb_voice.Progress = result;
		}

		public void changeBrightness (float distanceYY, float brightness)
		{
			//calculate brightness change 
			float moveBrightness = -distanceYY * maxBrightnessScreenHeightScale;

			float result = brightness + moveBrightness;
			if (result < 0) {
				result = 0;
			} else if (result > 0.8f)
				result = 0.8f;
			//add brightness
			ViewHelper.SetAlpha (view_brightness, result);
		}

		/// <summary>
		/// show or hide the control
		/// </summary>
		public void toggleCtlLayout ()
		{
			float translationY = ViewHelper.GetTranslationY (ll_top_ctrl);
			//show->hide
			if (translationY == 0) {
				NineOldAndroids.View.ViewPropertyAnimator.Animate (ll_top_ctrl).TranslationY (-ll_top_ctrl.Height);
				//bottom hide
				NineOldAndroids.View.ViewPropertyAnimator.Animate (ll_bottom_ctrl).TranslationY (ll_bottom_ctrl.Height);
			} else {
				//top show
				NineOldAndroids.View.ViewPropertyAnimator.Animate (ll_top_ctrl).TranslationY (0f);
				NineOldAndroids.View.ViewPropertyAnimator.Animate (ll_bottom_ctrl).TranslationY (0f);
				sendCtlHideMessage ();
			}
		}

		/// <summary>
		/// /*Sends the ctl hide message.*/
		/// </summary>
		private void sendCtlHideMessage ()
		{
			removeCtlHideMessage ();
			handler.SendEmptyMessageDelayed (Constants.HIDE_CTRL_LAYOUT, 5000);
		}

		/// <summary>
		/// Removes the ctl hide message.
		/// </summary>
		private void removeCtlHideMessage ()
		{
			handler.RemoveMessages (Constants.HIDE_CTRL_LAYOUT);
		}

		/// <param name="newConfig">The new device configuration.</param>
		/// <summary>
		/// Called by the system when the device configuration changes while your
		///  component is running.
		/// </summary>
		public override void OnConfigurationChanged (Android.Content.Res.Configuration newConfig)
		{
			base.OnConfigurationChanged (newConfig);
			if (newConfig.Orientation == Android.Content.Res.Orientation.Portrait) {
				ll_comment.Visibility = ViewStates.Visible;
				
			} else if (newConfig.Orientation == Android.Content.Res.Orientation.Landscape) {
				ll_comment.Visibility = ViewStates.Gone;
			}
		}

		public override void OnBackPressed ()
		{
			//back pressed
			DialogFactory.toastNegativePositiveDialog (this, "Return", "You are watching the video, are you sure to return to the video list?", 4);
		}
	}

	/// <summary>
	/// handle message
	/// </summary>
	public class MyHandler:Handler
	{
		private TextView tv_system_time;
		private TextView tv_position;
		private SeekBar sb;
		private IO.Vov.Vitamio.Widget.VideoView vv;
		private VideoPlayerActivity vp;

		public MyHandler (TextView tv_system_time, TextView tv_position, IO.Vov.Vitamio.Widget.VideoView vv, SeekBar sb, VideoPlayerActivity vp)
		{
			this.tv_system_time = tv_system_time;
			this.tv_position = tv_position;
			this.vv = vv;
			this.sb = sb;
			this.vp = vp;
		}

		public override void HandleMessage (Message msg)
		{
			base.HandleMessage (msg);
			switch (msg.What) {
			case 0://update system time
				tv_system_time.Text = DateFormat.Format ("kk:mm:ss", Java.Lang.JavaSystem.CurrentTimeMillis ());
				SendEmptyMessageDelayed (Constants.UPDATE_SYSTEM_TIME, 1000);
				break;
			case 1://update current position
				tv_position.Text = Utils.formatMillis (vv.CurrentPosition);
				sb.Progress = (int)vv.CurrentPosition;
				SendEmptyMessageDelayed (Constants.UPDATE_CURRENT_POSITION, 300);
				break;
			case 2://hide the control 
				vp.toggleCtlLayout ();
				break;
			default:
				break;
			}
		}
	}

	public class MyGestureListener:GestureDetector.SimpleOnGestureListener
	{
		private bool isLeftDown;
		private float brightness;
		private VideoPlayerActivity vp;
		private View v;

		public MyGestureListener (VideoPlayerActivity vp, View view)
		{
			this.vp = vp;
			this.v = view;
		}

		//on scrolling
		public override bool OnScroll (MotionEvent e1, MotionEvent e2, float distanceX, float distanceY)
		{
			float distanceYY = e1.GetY () - e2.GetY ();
			if (isLeftDown) {
				//change the brightness
				vp.changeBrightness (distanceYY, brightness);

			} else {
				vp.changeVolume (distanceYY);
			}
			return true;
		}

		//down
		public override bool OnDown (MotionEvent e)
		{
			vp.currentVolume = vp.GetStreamVolume ();
			isLeftDown = e.GetX () < Utils.getWindowWidth (Android.App.Application.Context) / 2;
			brightness = ViewHelper.GetAlpha (v);
			return base.OnDown (e);
		}

		/// <summary>
		/// 
		/// Double click full screen
		/// </summary>
		/// <param name="e">E.</param>


		public override bool OnDoubleTap (MotionEvent e)
		{
			vp.toggleFullscreen ();
			return true;
		}

		public override bool OnSingleTapUp (MotionEvent e)
		{
			vp.toggleCtlLayout ();
			return true;
		}

		public override void OnLongPress (MotionEvent e)
		{
			vp.play ();
		}
	}

	public class MyAnimatorListener:Java.Lang.Object,Animator.IAnimatorListener
	{
		public void OnAnimationCancel (Animator animation)
		{
			
		}

		public void OnAnimationEnd (Animator animation)
		{
			ll_loading.Visibility = ViewStates.Gone;
			ViewHelper.SetAlpha (ll_loading, 1.0f);
		}

		public void OnAnimationRepeat (Animator animation)
		{
			
		}

		public void OnAnimationStart (Animator animation)
		{
			
		}

		private LinearLayout ll_loading;

		public MyAnimatorListener (LinearLayout ll_loading)
		{
			this.ll_loading = ll_loading;
		}
			
	}
}
	