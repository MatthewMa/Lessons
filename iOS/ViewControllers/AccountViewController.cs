using System;
using UIKit;

namespace LessonBasket.iOS
{
    public class AccountViewController : UIViewController
    {
        public AccountViewController()
            : base()
        {
            TabBarItem.Image = UIImage.FromBundle("user.png");
            TabBarItem.Title = "Account";
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            View.BackgroundColor = UIColor.White;
        }
    }
}

