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
        public MainWindow()
        {
            InitializeComponent();

            mainCanvas.MouseDown += MainCanvas_GotFocus;

            // custom textbox for testing
            Blurb test = new Blurb(this);
            mainCanvas.Children.Add(test);
        }

        private void MainCanvas_GotFocus(object sender, RoutedEventArgs e)
        {
            Console.WriteLine(sender.GetType());
            commandLine.Text = "click?";
        }
    }
}
