using System;
using UIKit;
using System.Collections.Generic;

namespace LessonBasket.iOS
{
    public static class LessonScreenSelector
    {
        public static UIViewController Select(IList<Screen> screens, int index)
        {
            UIViewController result = null;

            switch (screens[index].type)
            {
                case "video":
                    result = new LessonVideoScreenViewController(screens, index);
                    break;
                default:
                    break;
            }

            return result;
        }
    }
}

