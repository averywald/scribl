using System;
using System.Collections.Generic;
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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<Blurb> blurbs = new List<Blurb>();
        private bool hasEditableBlurb // TODO: ensure that only one blurb is in edit mode at a time
        {
            get
            {
                foreach(Blurb b in this.blurbs)
                {
                    if (b.IsEditing) return true; // if any in editing mode
                }
                return false; // if none are in editing mode
            }
        }

        public MainWindow()
        {
            InitializeComponent();

            mainCanvas.MouseLeftButtonDown += MainCanvas_MouseLeftButtonDown; // attach event handler
        }

        private void MainCanvas_MouseLeftButtonDown(object sender, MouseButtonEventArgs args)
        {
            if (args.Source is Canvas)
            {
                if (args.ClickCount == 2) this.NewBlurb(sender, args); // double click - create new blurb

                else
                {
                    if (this.hasEditableBlurb)
                    {
                        int index = this.GetBlurbInEditMode();
                        Blurb b = this.blurbs[index];
                        b.IsEditing = false;
                    }
                }
            }
        }

        private int GetBlurbInEditMode()
        {
            foreach (Blurb b in this.blurbs)
            {
                if (b.IsEditing) return this.blurbs.IndexOf(b); // return index of the blurb in edit mode
            }
            return -1; // if none found
        }

        private void NewBlurb(object sender, MouseButtonEventArgs args)
        {
            Blurb b = new Blurb(this); // should pass click coords to add the blurb there
            blurbs.Add(b); // add to object collection
            mainCanvas.Children.Add(b); // add to UI

            // place in UI at position of click
            Canvas.SetLeft(b, args.GetPosition(this).X);
            Canvas.SetTop(b, args.GetPosition(this).Y);
        }
    }
}
