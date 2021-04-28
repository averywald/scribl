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

        public MainWindow()
        {
            InitializeComponent();

            mainCanvas.MouseDown += NewBlurb;

            // custom textbox for testing
            Blurb test = new Blurb(this);
            blurbs.Add(test);
            mainCanvas.Children.Add(test);
        }

        private void NewBlurb(object sender, MouseButtonEventArgs args)
        {
            if (args.Source is Canvas)
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
}
