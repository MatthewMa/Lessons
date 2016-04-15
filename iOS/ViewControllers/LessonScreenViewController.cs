using System;
using UIKit;
using System.Collections.Generic;

namespace LessonBasket.iOS
{
    public class LessonScreenViewController : UIViewController
    {
        protected IList<Screen> Screens { get; set; }

        protected int Index { get; set; }

        public LessonScreenViewController(IList<Screen> screens, int index)
        {
            Screens = screens;
            Index = index;
        }

        protected virtual void PushNextScreen()
        {
            if (Index > Screens.Count - 1)
            {
                return;
            }

            var lessonScreen = LessonScreenSelector.Select(Screens, Index + 1);
            if (lessonScreen != null)
            {
                NavigationController.PushViewController(lessonScreen, true);
            }
        }
    }
}

