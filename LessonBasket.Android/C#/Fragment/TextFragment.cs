
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using LessonBasket;
using Android.Media;
using Java.IO;

namespace LessonBasketDemo
{
	public class TextFragment : Fragment
	{
		public string text { get { return Arguments.GetString ("text", "Default Text"); } }

		private Button btn_record;

		private MediaRecorder recorder;

		private Handler handler;

		private ImageView iv_record;

		public override View OnCreateView (LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
		{
			// Use this to return your custom view for this Fragment
			// return inflater.Inflate(Resource.Layout.YourFragment, container, false);
			base.OnCreateView (inflater, container, savedInstanceState);
			var view = inflater.Inflate (Resource.Layout.fragment_text, container, false);
			view.FindViewById<TextView> (Resource.Id.tv_title).Text = text;
			btn_record = view.FindViewById<Button> (Resource.Id.btn_record);
			iv_record = view.FindViewById<ImageView> (Resource.Id.iv_record);
			iv_record.SetImageResource (Resource.Drawable.record);
			btn_record.Click += delegate(object sender, EventArgs e) {
				//begin record
				recorder = new MediaRecorder ();
				recorder.SetAudioSource (AudioSource.Mic);
				recorder.SetOutputFormat (OutputFormat.RawAmr);
				recorder.SetAudioEncoder (AudioEncoder.AmrNb);
				var audioFile = File.CreateTempFile ("record_", "amr");
				recorder.SetOutputFile (audioFile.AbsolutePath);
				recorder.Prepare ();
				recorder.Start ();
				iv_record.SetImageResource (Resource.Drawable.recording);
				btn_record.Enabled = false;
				handler = new Handler ();
				handler.PostDelayed (delegate() {
					//close the recorder
					recorder.Stop ();
					recorder.Release ();
					btn_record.Enabled = true;
					iv_record.SetImageResource (Resource.Drawable.record);
				}, 15000);
			};
			return view;
		}

		public static TextFragment NewInstance (Screen screen)
		{
			var textFrag = new TextFragment{ Arguments = new Bundle () };
			textFrag.Arguments.PutString ("text", screen.Text);
			return textFrag;
		}


	}
}

