using System;
using UIKit;
using System.Collections.Generic;

namespace LessonBasket.iOS
{
    public class LessonsViewController : UIViewController
    {
        public UITableView LessonsTable { get; set; }

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

            LessonsTable = new UITableView();
            Add(LessonsTable); // alias to View.AddSubview()

            var lesson1 = new Lesson();
            lesson1.title = "Clay Debray & the Muskrat";
            var lesson2 = new Lesson();
            lesson2.title = "Jason Horse, Cree Soccer";

            var items = new List<Tuple<Lesson, bool>>
            {
                new Tuple<Lesson, bool>(lesson1, true),
                new Tuple<Lesson, bool>(lesson2, false),
            };

            LessonsTable.Source = new LessonTableSource(this, items);

            View.ConstrainLayout(() =>
                LessonsTable.Frame.Top == View.Frame.Top &&
                LessonsTable.Frame.Left == View.Frame.Left &&
                LessonsTable.Frame.Right == View.Frame.Right &&
                LessonsTable.Frame.Bottom == View.Frame.Bottom
            );
        }
    }

    public class LessonTableSource : UITableViewSource
    {
        public UIViewController Container { get; private set; }

        public List<Tuple<Lesson, bool>> Items { get; set; }

        private string cellIdentifier = "TableCell";

        public LessonTableSource(UIViewController container, List<Tuple<Lesson, bool>> items)
        {
            Container = container;
            Items = items;
        }

        public override nint RowsInSection(UITableView tableview, nint section)
        {
            return Items.Count;
        }

        public override void RowSelected(UITableView tableView, Foundation.NSIndexPath indexPath)
        {
            throw new System.NotImplementedException();
        }

        public override UITableViewCell GetCell(UITableView tableView, Foundation.NSIndexPath indexPath)
        {
            var cell = tableView.DequeueReusableCell(cellIdentifier);

            if (cell == null)
            {
                cell = new UITableViewCell(UITableViewCellStyle.Default, cellIdentifier);
            }

            // If lesson is finished then add check mark
            if (Items[indexPath.Row].Item2)
            {
                cell.Accessory = UITableViewCellAccessory.Checkmark;
            }

            cell.TextLabel.Text = Items[indexPath.Row].Item1.title;

            return cell;
        }
    }
}

