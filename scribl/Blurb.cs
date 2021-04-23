using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace scribl
{
    public partial class Blurb : TextBlock
    {
        // override textblock behavior for
        // hover
        // double click
        // focused state
        // editable focused state
        // draggable semi-focused state

        static Blurb() // override default styling
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(Blurb),
                   new FrameworkPropertyMetadata(typeof(Blurb)));
        }

        // public Blurb()
    }
}
