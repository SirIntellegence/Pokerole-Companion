using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace PokeroleUI2
{
    class HorizontalContentBlock : ContentControl
    {
        static HorizontalContentBlock()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(HorizontalContentBlock),
           new FrameworkPropertyMetadata(typeof(HorizontalContentBlock)));
        }
    }
}
