using System;
using UIKit;
using CoreGraphics;

namespace LessonBasket.iOS
{
    public class LoginViewController : UIViewController
    {
        public LoginViewController()
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            Title = "Login";
            View.BackgroundColor = UIColor.FromHSB(UIConstants.BackgroundColorHue, UIConstants.BackgroundColorSaturation, UIConstants.BackgroundColorBrightness);

            var logo = new UIImageView();
            View.AddSubview(logo);
            logo.Image = UIImage.FromBundle("lesson_basket_logo.png");
            logo.Layer.BorderColor = new CGColor(255, 255, 255);
            logo.Layer.BorderWidth = 1f;
            logo.Layer.CornerRadius = 25f;

            var username = new UITextField
            {
                Placeholder = "Email",
                BorderStyle = UITextBorderStyle.RoundedRect,
            };
            View.AddSubview(username);
            username.Layer.BorderColor = UIColor.Gray.CGColor;
            username.Layer.BorderWidth = UIConstants.BorderWidth;
            username.Layer.CornerRadius = UIConstants.CornerRadius;

            var password = new UITextField
            {
                Placeholder = "Password",
                BorderStyle = UITextBorderStyle.RoundedRect,
                SecureTextEntry = true,
            };
            View.AddSubview(password);
            password.Layer.BorderColor = UIColor.Gray.CGColor;
            password.Layer.BorderWidth = UIConstants.BorderWidth;
            password.Layer.CornerRadius = UIConstants.CornerRadius;

            var loginButton = new UIButton(UIButtonType.System)
            {
                BackgroundColor = UIColor.FromHSB(UIConstants.BackgroundColorHue + 0.08f, UIConstants.BackgroundColorSaturation + 0.1f, UIConstants.BackgroundColorBrightness),
                Font = UIFont.BoldSystemFontOfSize(UIConstants.NormalFontSize),
            };
            View.AddSubview(loginButton);
            loginButton.SetTitle("Login", UIControlState.Normal);
            loginButton.SetTitleColor(UIColor.White, UIControlState.Normal);
            loginButton.Layer.CornerRadius = UIConstants.CornerRadius;
            loginButton.TouchUpInside += (sender, e) =>
            {
                NavigationController.PushViewController(new TabViewController(), true);
            };

            var signUpButton = new UIButton(UIButtonType.System)
            {
                BackgroundColor = UIColor.White,
                Font = UIFont.BoldSystemFontOfSize(UIConstants.NormalFontSize),
            };
            View.AddSubview(signUpButton);
            signUpButton.SetTitle("Sign Up", UIControlState.Normal);
            signUpButton.SetTitleColor(UIColor.FromHSB(UIConstants.BackgroundColorHue, UIConstants.BackgroundColorSaturation, UIConstants.BackgroundColorBrightness), UIControlState.Normal);
            signUpButton.Layer.CornerRadius = UIConstants.CornerRadius;

            #region UI Layout
//            var topPad = (float)NavigationController.NavigationBar.Frame.Size.Height + UIConstants.VerticalPad + 30f;
            var topPad = View.Frame.GetCenterY() - 170f - 100f; // 170 is half the total heights of all controls

            View.ConstrainLayout(() =>
                logo.Frame.Top == View.Frame.Top + topPad &&
                logo.Frame.Left >= View.Frame.Left + UIConstants.HorizontalPad &&
                logo.Frame.Right >= View.Frame.Right - UIConstants.HorizontalPad &&
                logo.Frame.GetCenterX() == View.Frame.GetCenterX() &&
                logo.Frame.Width == 500f &&

                username.Frame.Top == logo.Frame.Bottom + 40f &&
                username.Frame.Left >= View.Frame.Left + UIConstants.HorizontalPad &&
                username.Frame.Right >= View.Frame.Right - UIConstants.HorizontalPad &&
                username.Frame.Height == UIConstants.ControlsHeight &&
                username.Frame.GetCenterX() == View.Frame.GetCenterX() &&
                username.Frame.Width <= UIConstants.MaximumControlsWidth &&

                password.Frame.Top == username.Frame.Bottom + 10f &&
                password.Frame.Left >= View.Frame.Left + UIConstants.HorizontalPad &&
                password.Frame.Right >= View.Frame.Right - UIConstants.HorizontalPad &&
                password.Frame.Height == UIConstants.ControlsHeight &&
                password.Frame.GetCenterX() == View.Frame.GetCenterX() &&
                password.Frame.Width <= UIConstants.MaximumControlsWidth &&

                loginButton.Frame.Top == password.Frame.Bottom + 40f &&
                loginButton.Frame.Left >= View.Frame.Left + UIConstants.HorizontalPad &&
                loginButton.Frame.Right >= View.Frame.Right - UIConstants.HorizontalPad &&
                loginButton.Frame.Height == UIConstants.ControlsHeight &&
                loginButton.Frame.GetCenterX() == View.Frame.GetCenterX() &&
                loginButton.Frame.Width <= UIConstants.MaximumControlsWidth &&

                signUpButton.Frame.Top == loginButton.Frame.Bottom + 10f &&
                signUpButton.Frame.Left >= View.Frame.Left + UIConstants.HorizontalPad &&
                signUpButton.Frame.Right >= View.Frame.Right - UIConstants.HorizontalPad &&
                signUpButton.Frame.Height == UIConstants.ControlsHeight &&
                signUpButton.Frame.GetCenterX() == View.Frame.GetCenterX() &&
                signUpButton.Frame.Width <= UIConstants.MaximumControlsWidth
            );
            #endregion
        }
    }
}