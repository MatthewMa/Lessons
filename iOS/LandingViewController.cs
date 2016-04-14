using System;
using UIKit;

namespace LessonBasket.iOS
{
    public class LandingViewController : UIViewController
    {
        public LandingViewController()
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            View.BackgroundColor = UIColor.FromPatternImage(UIImage.FromBundle("parade.png"));

            var button = new UIButton(UIButtonType.System)
            {
                BackgroundColor = UIColor.FromHSB(UIConstants.BackgroundColorHue, UIConstants.BackgroundColorSaturation, UIConstants.BackgroundColorBrightness),
                Font = UIFont.BoldSystemFontOfSize(20),
            };
            View.AddSubview(button);
            button.SetTitle("GET STARTED", UIControlState.Normal);
            button.SetTitleColor(UIColor.White, UIControlState.Normal);
            button.Layer.CornerRadius = 30f;
            button.TouchUpInside += (sender, e) =>
            {
                NavigationController.PushViewController(new LoginViewController(), true);
            };

            View.ConstrainLayout(() =>
                button.Frame.Top == View.Frame.GetCenterY() + 50f &&
                button.Frame.Left >= View.Frame.Left + UIConstants.HorizontalPad &&
                button.Frame.Right >= View.Frame.Right - UIConstants.HorizontalPad &&
                button.Frame.GetCenterX() == View.Frame.GetCenterX() &&
                button.Frame.Height == 70f &&
                button.Frame.Width == 300f 
            );
        }
    }
}

