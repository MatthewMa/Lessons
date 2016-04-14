using System;
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

            View.BackgroundColor = UIColor.White;
        }
    }
}
