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

            if (Index < Screens.Count - 1)
            {
                NavigationItem.SetRightBarButtonItem(new UIBarButtonItem("Next", UIBarButtonItemStyle.Plain, ((sender, e) =>
                        {
                            PushNextScreen();
                        })), true);
            }
            else
            {
                NavigationItem.SetRightBarButtonItem(new UIBarButtonItem("Submit", UIBarButtonItemStyle.Plain, ((sender, e) =>
                        {
                        })), true); 
            }

            var player = new AVPlayer(NSUrl.FromString(Screens[Index].video_url));
            var playerViewController = new AVPlayerViewController
            {
                Player = player,      
            };
            AddChildViewController(playerViewController);
            View.AddSubview(playerViewController.View);

            #region Layout
            View.ConstrainLayout(() =>
                playerViewController.View.Frame.Top == View.Frame.Top &&
                playerViewController.View.Frame.Left == View.Frame.Left &&
                playerViewController.View.Frame.Right == View.Frame.Right &&
                playerViewController.View.Frame.Bottom == View.Frame.Bottom
            );
            #endregion
        }
    }
}

