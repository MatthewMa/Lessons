using System;
using UIKit;
using System.Collections.Generic;

namespace LessonBasket.iOS
{
    public static class LessonScreenSelector
    {
        public static UIViewController Select(IList<Screen> screens, int index)
        {
            if (index > screens.Count - 1)
            {
                return null;
            }

            UIViewController result = null;

            switch (screens[index].type)
            {
                case "video":
                    result = new LessonVideoScreenViewController(screens, index);
                    break;
                case "audio_question":
                    break;
                default:
                    result = new UIViewController();
                    result.View.BackgroundColor = UIColor.White;
                    break;
            }

            return result;
        }
    }
}

