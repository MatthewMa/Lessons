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
        public IList<Tuple<UIButton, UIButton>> QuestionsUIs { get; set; }

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
            var topPad = (float)NavigationController.NavigationBar.Frame.Size.Height + 20f;

            View.ConstrainLayout(() =>
                playerViewController.View.Frame.Top == View.Frame.Top + topPad &&
                playerViewController.View.Frame.Left == View.Frame.Left &&
                playerViewController.View.Frame.Right == View.Frame.Right &&
                playerViewController.View.Frame.Height == 200f
            );
            #endregion

            QuestionsUIs = new List<Tuple<UIButton, UIButton>>();

            var i = 1;
            foreach (var option in Screens[Index].questions)
            {
                var button = new UIButton(UIButtonType.RoundedRect);
                View.AddSubview(button);
                button.SetImage(UIImage.FromBundle("radio_enable.png"), UIControlState.Normal);
                button.SetImage(UIImage.FromBundle("radio_disable.png"), UIControlState.Disabled);
                if (i != 1)
                {
                    button.Enabled = false;
                }

                var textButton = new UIButton(UIButtonType.System);
                View.AddSubview(textButton);
                textButton.SetTitle(option, UIControlState.Normal);

                QuestionsUIs.Add(new Tuple<UIButton, UIButton>(button, textButton));

                var optionTopPad = 300f + 50f * i;
                var leftPad = 400f;
                View.ConstrainLayout(() =>
                    button.Frame.Top == View.Frame.Top + optionTopPad &&
                    button.Frame.Left == View.Frame.Left + leftPad &&
                    button.Frame.Height == 20f &&
                    button.Frame.Width == 20f &&

                    textButton.Frame.GetCenterY() == button.Frame.GetCenterY() &&
                    textButton.Frame.Left == button.Frame.Left + 30f &&
                    textButton.Frame.Height == UIConstants.ControlsHeight
                );

                i++;
            }

            i = 0;
            foreach (var tuple in QuestionsUIs)
            {
                tuple.Item2.TouchUpInside += (sender, e) =>
                {
                    tuple.Item1.Enabled = true;

                    foreach (var otherTuple in QuestionsUIs)
                    {
                        if (!Object.ReferenceEquals(sender, otherTuple.Item2))
                        {
                            otherTuple.Item1.Enabled = false;
                        }
                    }
                };
                i++;
            }
        }
    }
}

