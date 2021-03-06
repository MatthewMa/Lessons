﻿using System;
using UIKit;

namespace LessonBasket.iOS
{
    public class SettingsViewController : UIViewController
    {
        public SettingsViewController()
            : base()
        {
            TabBarItem.Image = UIImage.FromBundle("preferences.png");
            TabBarItem.Title = "Settings";
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            var signOutButton = new UIButton(UIButtonType.RoundedRect)
            {
                BackgroundColor = UIColor.Red,
                Font = UIFont.BoldSystemFontOfSize(UIConstants.NormalFontSize),
            };
            View.AddSubview(signOutButton);
            signOutButton.SetTitle("Sign Out", UIControlState.Normal);
            signOutButton.SetTitleColor(UIColor.White, UIControlState.Normal);
            signOutButton.Layer.CornerRadius = UIConstants.CornerRadius;

            View.BackgroundColor = UIColor.White;

            #region Layout
            View.ConstrainLayout(() =>
                signOutButton.Frame.Left == View.Frame.Left + 300f &&
                signOutButton.Frame.Right == View.Frame.Right - 300f &&
                signOutButton.Frame.Bottom == View.Frame.Bottom - 100f &&
                signOutButton.Frame.Height == UIConstants.ControlsHeight
            );
            #endregion
        }
    }
}
