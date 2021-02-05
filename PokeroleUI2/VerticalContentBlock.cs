using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace PokeroleUI2
{
    class VerticalContentBlock : ContentControl
    {
        static VerticalContentBlock()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(VerticalContentBlock),
           new FrameworkPropertyMetadata(typeof(VerticalContentBlock)));
        }
    }
}
