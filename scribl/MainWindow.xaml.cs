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
        Nullable<Point> dragStart = null; // will hold coordinates on drag event

        public MainWindow()
        {
            InitializeComponent();

            // example textbox
            // TextBox test = new TextBox();
            TextBlock test = new TextBlock();
            test.Text = "hello, i am a text box";
            test.MouseMove += OnMouseMove;

            mainCanvas.Children.Add(test);
        }

        void OnMouseMove(object sender, MouseEventArgs args)
        {
            if (args.Source is TextBlock tb)
            {
                if (args.LeftButton == MouseButtonState.Pressed)
                {
                    Point p = args.GetPosition(mainCanvas);
                    Canvas.SetLeft(tb, p.X - tb.ActualWidth / 2);
                    Canvas.SetTop(tb, p.Y - tb.ActualHeight / 2);
                    tb.CaptureMouse();
                }
                else
                {
                    tb.ReleaseMouseCapture();
                }
            }
        }
    }
}
