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
        public Point Position; // handle
        public Dictionary<string, double> Dimensions // should be updated when resized
        {
            get => new Dictionary<string, double>
            {
                { "width", this.Width },
                { "height", this.Height }
            };
        }
        private FlowDocument document;
        private MainWindow parent;

        public Blurb(MainWindow parent) // constructor
        {
            this.parent = parent; // save parent ref for relative positioning?

            InitializeComponent(); // actually create the component in-app

            this.MouseMove += Blurb_MouseMove;

            this.textBlock.Text = "fake text for test";
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

        // more, definitely
    }
}
