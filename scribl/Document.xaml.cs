using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace scribl
{
    /// <summary>
    /// Interaction logic for Document.xaml
    /// </summary>
    public partial class Document : UserControl
    {
        public ObservableCollection<Blurb> Blurbs = new ObservableCollection<Blurb>();

        private bool hasEditableBlurb // TODO: ensure that only one blurb is in edit mode at a time
        {
            get
            {
                foreach (Blurb b in this.Blurbs)
                {
                    if (b.IsActive) return true; // if any in editing mode
                }
                return false; // if none are in editing mode
            }
        }

        public Document()
        {
            InitializeComponent();

            mainCanvas.MouseLeftButtonDown += MainCanvas_MouseLeftButtonDown; // attach event handler
        }

        private void MainCanvas_MouseLeftButtonDown(object sender, MouseButtonEventArgs args)
        {
            if (args.Source is Canvas)
            {
                if (args.ClickCount == 2) this.AddBlurb(sender, args); // double click - create new blurb

                else if (args.ClickCount == 1) // single click
                {
                    foreach (Blurb b in this.Blurbs)
                    {
                        b.DisableEditMode();
                    }
                }
            }
        }

        private void AddBlurb(object sender, MouseButtonEventArgs args)
        {
            Blurb b = new Blurb(this, this.Blurbs.Count); // should pass click coords to add the blurb there
            Blurbs.Add(b); // add to object collection
            mainCanvas.Children.Add(b); // add to UI

            // place in UI at position of click
            Canvas.SetLeft(b, args.GetPosition(this).X);
            Canvas.SetTop(b, args.GetPosition(this).Y);
        }

        private int GetActiveBlurb()
        {
            foreach (Blurb b in this.Blurbs)
            {
                if (b.IsActive) return this.Blurbs.IndexOf(b); // return index of the blurb in edit mode
            }
            return -1; // if none found
        }
    }
}
