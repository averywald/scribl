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
    /// Interaction logic for Blurb component
    /// displays text
    /// richly edit text
    /// moveable
    /// 
    /// TODO: CRUD
    /// </summary>
    public partial class Blurb : UserControl
    {
        public bool IsEditing
        {
            get => this.isEditing;
            set
            {
                this.isEditing = value;

                // TODO: eliminate side-effects ??
                if (value == true) this.EnableEditMode();
                else this.DisableEditMode();
            }
        }
        public Dictionary<string, double> Dimensions // should be updated when resized
        {
            get => new Dictionary<string, double>
            {
                { "width", this.Width },
                { "height", this.Height }
            };
        }
        public Point Position; // handle

        private bool isEditing;
        private FlowDocument document;
        private MainWindow parent;

        public Blurb(MainWindow parent) // constructor
        {
            this.parent = parent; // save parent ref for relative positioning?

            InitializeComponent(); // actually create the component in-app

            this.document = new FlowDocument(); // initialize rich text editor doc

            this.MouseMove += Blurb_MouseMove; // attach the drag event function
            this.MouseDoubleClick += Blurb_MouseDoubleClick; // attach the edit text mode function
            this.LostFocus += Blurb_LostFocus;

            this.textBlock.Text = "fake text for test";
        }

        private void Blurb_LostFocus(object sender, RoutedEventArgs e)
        {
            if (isEditing) this.DisableEditMode();
        }

        private void Blurb_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (!isEditing) this.EnableEditMode();
        }

        private void Blurb_MouseMove(object sender, MouseEventArgs args)
        {
            if (args.Source is Blurb b) // should check if args reference this obj instance specifically
            {
                if (args.LeftButton == MouseButtonState.Pressed)
                {
                    Point p = args.GetPosition(parent);

                    this.Position.X = p.X - b.ActualWidth / 2;
                    this.Position.Y = p.Y - b.ActualHeight / 2;

                    Canvas.SetLeft(b, this.Position.X);                    
                    Canvas.SetTop(b, this.Position.Y);

                    b.CaptureMouse();
                }
                else
                {
                    b.ReleaseMouseCapture();
                }
            }
        }

        private void DisableEditMode()
        {
            this.isEditing = false; // deactivate editor mode

            // TODO: process flow document, spit into textBlock

            textBox.Visibility = Visibility.Hidden; // hide rich text editor
            textBlock.Visibility = Visibility.Visible; // show textblock plaintext
        }

        private void EnableEditMode()
        {
            this.isEditing = true; // set the blurb's mode to editing
            textBlock.Visibility = Visibility.Hidden; // hide the textblock
            string text = textBlock.Text; // save the content of the box - TODO: should automatically update flowdoc
            textBox.Visibility = Visibility.Visible; // show rich text editor
        }
    }
}
