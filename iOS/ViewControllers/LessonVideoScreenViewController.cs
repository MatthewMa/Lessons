using System;
using UIKit;
using System.Collections.Generic;
using AVFoundation;
using Foundation;
using AVKit;

namespace LessonBasket.iOS
{
    public class LessonVideoScreenViewController : LessonScreenViewController
    {
        public LessonVideoScreenViewController(IList<Screen> screens, int index)
            : base(screens, index)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            View.BackgroundColor = UIColor.White;

            var player = new AVPlayer(NSUrl.FromString(Screens[Index].video_url));
            var playerViewController = new AVPlayerViewController
            {
                Player = player,      
            };
            AddChildViewController(playerViewController);
            View.AddSubview(playerViewController.View);

            #region Layout
            var topPad = (float)NavigationController.NavigationBar.Frame.Size.Height + 20f;

            View.ConstrainLayout(() =>
                playerViewController.View.Frame.Top == View.Frame.Top + topPad &&
                playerViewController.View.Frame.Left == View.Frame.Left &&
                playerViewController.View.Frame.Right == View.Frame.Right &&
                playerViewController.View.Frame.Bottom == View.Frame.Bottom
            );
            #endregion
        }
    }
}

