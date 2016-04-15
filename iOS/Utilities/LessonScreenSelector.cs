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
                case "1":
                    result = new LessonVideoScreenViewController(screens, index);
                    break;
                case "2":
                    break;
                default:
                    break;
            }

            return result;
        }
    }
}

