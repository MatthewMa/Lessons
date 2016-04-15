using System;
using UIKit;
using System.Collections.Generic;
using AVFoundation;
using Foundation;
using AVKit;

namespace LessonBasket.iOS
{
    public class LessonAudioScreenViewController : LessonScreenViewController
    {
        public LessonAudioScreenViewController(IList<Screen> screens, int index)
            : base(screens, index)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            View.BackgroundColor = UIColor.White;

            var player = new AVPlayer(NSUrl.FromString(Screens[Index].audio_url));
            var playerViewController = new AVPlayerViewController
            {
                Player = player,      
            };
            AddChildViewController(playerViewController);
            View.AddSubview(playerViewController.View);

            #region Layout
            var navHeight = (float)NavigationController.NavigationBar.Frame.Size.Height;

            View.ConstrainLayout(() =>
                playerViewController.View.Frame.Top == View.Frame.Top + navHeight &&
                playerViewController.View.Frame.Left == View.Frame.Left &&
                playerViewController.View.Frame.Right == View.Frame.Right &&
                playerViewController.View.Frame.Height == 200f
            );
            #endregion

            var i = 1;

            foreach (var option in Screens[Index].questions)
            {
                var label = new UILabel
                {
                    Text = option,
                };
                View.AddSubview(label);

                var topPad = 300f + 50f * i;
                var leftPad = 400f;
                i++;
                View.ConstrainLayout(() =>
                    label.Frame.Top == View.Frame.Top + topPad &&
                    label.Frame.Left == View.Frame.Left + leftPad &&
                    label.Frame.Height == UIConstants.ControlsHeight
                );
            }
        }
    }
}

