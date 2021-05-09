using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    public partial class Blurb : UserControl, INotifyPropertyChanged
    {
        #region Properties
        public Point Position; // handle
        public bool IsActive
        {
            get => this.isActive;
            set
            {
                if (this.isActive != value) // when isActive state changes
                {
                    this.isActive = value;
                    this.NotifyActiveStateChanged(this.id); // notify listeners
                }
            }
        }
        #endregion

        #region Attributes
        public event PropertyChangedEventHandler PropertyChanged; // handler to notify listeners of active state changes
        private int id;
        private bool isActive = true;
        private bool isEditing = true; // initally true - when a blurb is created it has focus
        private FlowDocument document;
        private Document parent;
        #endregion

        public Blurb(Document parent, int id) // constructor
        {
            this.parent = parent; // save parent ref for relative positioning?

            InitializeComponent(); // actually create the component in-app

            this.document = new FlowDocument(); // initialize rich text editor doc

            this.MouseLeftButtonDown += Blurb_MouseLeftButtonDown;
            this.MouseMove += Blurb_MouseMove; // attach the drag event function
            this.MouseDoubleClick += Blurb_MouseDoubleClick; // attach the edit text mode function
        }

        public void NotifyActiveStateChanged(int blurbId)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(blurbId.ToString()));
            }
        }

        #region Event Handlers

        private void Blurb_MouseLeftButtonDown(object sender, MouseButtonEventArgs args)
        {
            if (args.Source is Blurb b && this.isActive == false)
            {
                this.IsActive = true;
                this.border.BorderBrush = Brushes.Red; // highlight border in UI
            }
        }

        private void Blurb_MouseDoubleClick(object sender, MouseButtonEventArgs args)
        {
            if (!isEditing) this.EnableEditMode();
        }

        private void Blurb_MouseMove(object sender, MouseEventArgs args)
        {
            if (args.Source is Blurb b && this.isEditing == false) // should check if args reference this obj instance specifically
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

        #endregion

        public void DisableEditMode()
        {
            this.isEditing = false; // deactivate editor mode
            this.IsActive = false;
            this.border.BorderBrush = Brushes.Gray; // un-highlight border in UI

            // TODO: process flow document, spit into textBlock

            textBox.Visibility = Visibility.Hidden; // hide rich text editor
            textBlock.Visibility = Visibility.Visible; // show textblock plaintext
        }

        public void EnableEditMode()
        {
            this.isEditing = true; // set the blurb's mode to editing
            this.IsActive = false;
            textBlock.Visibility = Visibility.Hidden; // hide the textblock
            string text = textBlock.Text; // save the content of the box - TODO: should automatically update flowdoc
            textBox.Visibility = Visibility.Visible; // show rich text editor
        }
    }
}
