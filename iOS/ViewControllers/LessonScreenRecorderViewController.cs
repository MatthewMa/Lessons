using System;
using System.Collections.Generic;
using UIKit;
using AVFoundation;
using Foundation;
using System.IO;
using System.Diagnostics;
using AudioToolbox;

namespace LessonBasket.iOS
{
    public class LessonScreenRecorderViewController : LessonScreenBaseViewController
    {
        public AVAudioRecorder Recorder { get; set; }

        public AVPlayer Player { get; set; }

        public NSDictionary Settings { get; set; }

        public NSUrl AudioFilePath { get; set; }

        public NSObject Observer;

        public Stopwatch Stopwatch { get; set; }

        public LessonScreenRecorderViewController(IList<Screen> screens, int index)
            : base(screens, index)
        {
            Stopwatch = null;
            AudioFilePath = null;

            AudioSession.Initialize(); // Required to activate recording
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            View.BackgroundColor = UIColor.White;

            var recordingStatusLabel = new UILabel
            {
                Text = "",
                TextColor = UIConstants.MainColor,
            };
            View.AddSubview(recordingStatusLabel);

            var lengthOfRecordingLabel = new UILabel
            {
                Text = "",
                TextColor = UIConstants.MainColor,
            };
            View.AddSubview(lengthOfRecordingLabel);

            var startRecordingButton = new UIButton
            {
            };
            View.AddSubview(startRecordingButton);
            startRecordingButton.SetTitle("Start", UIControlState.Normal);
            startRecordingButton.SetTitleColor(UIConstants.MainColor, UIControlState.Normal);

            var stopRecordingButton = new UIButton
            {
            };
            View.AddSubview(stopRecordingButton);
            stopRecordingButton.SetTitle("Stop", UIControlState.Normal);
            stopRecordingButton.SetTitleColor(UIConstants.MainColor, UIControlState.Normal);

            #region Layout
            var topPad = (float)NavigationController.NavigationBar.Frame.Size.Height + 20f;

            View.ConstrainLayout(() =>
                startRecordingButton.Frame.Top == View.Frame.Top + topPad &&
                startRecordingButton.Frame.Left == View.Frame.Left + 30f &&
                startRecordingButton.Frame.Height == UIConstants.ControlsHeight &&
                startRecordingButton.Frame.Width == 100f &&

                recordingStatusLabel.Frame.Top == View.Frame.Top + topPad &&
                recordingStatusLabel.Frame.Left == startRecordingButton.Frame.Right + 30f &&
                recordingStatusLabel.Frame.Height == UIConstants.ControlsHeight &&
                recordingStatusLabel.Frame.Width == 100f &&

                stopRecordingButton.Frame.Top == startRecordingButton.Frame.Bottom + 30f &&
                stopRecordingButton.Frame.Left == View.Frame.Left + 30f &&
                stopRecordingButton.Frame.Height == UIConstants.ControlsHeight &&
                stopRecordingButton.Frame.Width == 100f &&

                lengthOfRecordingLabel.Frame.Top == startRecordingButton.Frame.Bottom + 30f &&
                lengthOfRecordingLabel.Frame.Left == stopRecordingButton.Frame.Right + 30f &&
                lengthOfRecordingLabel.Frame.Height == UIConstants.ControlsHeight &&
                lengthOfRecordingLabel.Frame.Width == 100f
            );
            #endregion
        }

        public override void ViewDidUnload()
        {
            base.ViewDidUnload();

            NSNotificationCenter.DefaultCenter.RemoveObserver(Observer);
        }

        public bool PrepareAudioRecording()
        {
            // You must initialize an audio session before trying to record
            var audioSession = AVAudioSession.SharedInstance();
            var err = audioSession.SetCategory(AVAudioSessionCategory.PlayAndRecord);
            if (err != null)
            {
                // Display Alert if needed !!
                return false;
            }
            err = audioSession.SetActive(true);
            if (err != null)
            {
                // Display Alert if needed !!
                return false;
            }

            // Declare string for application temp path and tack on the file extension
            string fileName = string.Format("Myfile{0}.aac", DateTime.Now.ToString("yyyyMMddHHmmss"));
            string tempRecording = Path.Combine(Path.GetTempPath(), fileName);

            Console.WriteLine(tempRecording);
            AudioFilePath = NSUrl.FromFilename(tempRecording);

            //set up the NSObject Array of values that will be combined with the keys to make the NSDictionary
            NSObject[] values = new NSObject[]
            {    
                NSNumber.FromFloat(44100.0f),
                NSNumber.FromInt32((int)AudioToolbox.AudioFormatType.MPEG4AAC),
                NSNumber.FromInt32(1),
                NSNumber.FromInt32((int)AVAudioQuality.High)
            };
            //Set up the NSObject Array of keys that will be combined with the values to make the NSDictionary
            NSObject[] keys = new NSObject[]
            {
                AVAudioSettings.AVSampleRateKey,
                AVAudioSettings.AVFormatIDKey,
                AVAudioSettings.AVNumberOfChannelsKey,
                AVAudioSettings.AVEncoderAudioQualityKey
            };          
            //Set Settings with the Values and Keys to create the NSDictionary
            Settings = NSDictionary.FromObjectsAndKeys(values, keys);

            //Set recorder parameters
            NSError error;
            Recorder = AVAudioRecorder.Create(this.AudioFilePath, new AudioSettings(Settings), out error);
            if ((Recorder == null) || (error != null))
            {
                // Display Alert if needed !!
                return false;
            }

            //Set Recorder to Prepare To Record
            if (!Recorder.PrepareToRecord())
            {
                Recorder.Dispose();
                Recorder = null;
                return false;
            }

            Recorder.FinishedRecording += delegate (object sender, AVStatusEventArgs e)
            {
                Recorder.Dispose();
                Recorder = null;
            };

            return true;
        }
    }
}

