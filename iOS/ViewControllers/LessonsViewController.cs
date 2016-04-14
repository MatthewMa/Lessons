using System;
using UIKit;

namespace LessonBasket.iOS
{
    public class LessonsViewController : UIViewController
    {
        public LessonsViewController()
            : base()
        {
            TabBarItem.Image = UIImage.FromBundle("book.png");
            TabBarItem.Title = "Lessons";
        }

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(true);

            View.BackgroundColor = UIColor.White;
        }
    }
}

