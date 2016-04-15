using System;
using UIKit;
using System.Collections.Generic;

namespace LessonBasket.iOS
{
    public class LessonAudioScreenViewController : UIViewController
    {
        public IList<Screen> Screens { get; set; }

        public int Index { get; set; }

        public LessonAudioScreenViewController(IList<Screen> screens, int index)
        {
            Screens = screens;
            Index = index;
        }
    }
}

