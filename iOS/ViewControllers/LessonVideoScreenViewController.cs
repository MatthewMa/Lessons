using System;
using UIKit;
using System.Collections.Generic;
using AVFoundation;
using Foundation;
using AVKit;

namespace LessonBasket.iOS
{
    public class LessonVideoScreenViewController : UIViewController
    {
        public IList<Screen> Screens { get; set; }

        public int Index { get; set; }

        public LessonVideoScreenViewController(IList<Screen> screens, int index)
        {
            Screens = screens;
            Index = index;
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            View.BackgroundColor = UIColor.White;

            NavigationItem.SetRightBarButtonItem(new UIBarButtonItem("Next", UIBarButtonItemStyle.Plain, ((sender, e) =>
                    {
                    })), true);

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

