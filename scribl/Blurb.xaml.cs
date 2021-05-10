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
                this.isActive = value; // assign new value
                if (value == true) // when the blurb becomes active
                {
                    this.NotifyActiveStateChanged(); // notify listener in document parent
                    this.border.BorderBrush = Brushes.Aqua; // highlight border
                }
                if (value == false)
                {
                    this.DisableEditMode();
                    this.border.BorderBrush = Brushes.Gray; // un-highlight border in UI
                }
            }
        }
        #endregion

        #region Attributes
        public event PropertyChangedEventHandler PropertyChanged; // handler to notify listeners of active state changes
        private bool isActive;
        private bool isEditing;
        private FlowDocument document;
        private Document parent;
        #endregion

        public Blurb(Document parent) // constructor
        {
            InitializeComponent(); // actually create the component in-app

            this.parent = parent; // save parent ref for relative positioning?

            // initally true - when a blurb is created it has focus
            this.IsActive = true;
            this.EnableEditMode();

            this.document = new FlowDocument(); // initialize rich text editor doc

            this.MouseLeftButtonDown += Blurb_MouseLeftButtonDown;
            this.MouseMove += Blurb_MouseMove; // attach the drag event function
            this.MouseDoubleClick += Blurb_MouseDoubleClick; // attach the edit text mode function
        }

        public void NotifyActiveStateChanged()
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(this.ToString())); // invoke event for the parent document
            }
        }

        #region Event Handlers

        private void Blurb_MouseLeftButtonDown(object sender, MouseButtonEventArgs args) 
        {
            if (args.Source is Blurb b && this.isActive == false)
            {
                this.IsActive = true;
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

        private void DisableEditMode()
        {
            this.isEditing = false; // deactivate editor mode
            
            //textBox.Visibility = Visibility.Hidden; // hide rich text editor
            this.document = textBox.Document;
            this.textBox.Focusable = false;
            this.textBox.IsReadOnly = true;
            //documentViewer.Document = this.document;
            //documentViewer.Visibility = Visibility.Visible; // show textblock plaintext
        }

        private void EnableEditMode()
        {
            if (this.document != null)
            {
                this.textBox.Document = this.document;
            }
            this.isEditing = true; // set the blurb's mode to editing
            this.textBox.Focusable = true;
            this.textBox.Focus();
            this.textBox.IsReadOnly = false;
            //documentViewer.Visibility = Visibility.Hidden; // hide the textblock
            //textBox.Visibility = Visibility.Visible; // show rich text editor
        }
    }
}
