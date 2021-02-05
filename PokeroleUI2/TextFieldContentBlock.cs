using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace PokeroleUI2
{
    class TextFieldContentBlock : ContentControl
    {
        static TextFieldContentBlock()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(TextFieldContentBlock),
           new FrameworkPropertyMetadata(typeof(TextFieldContentBlock)));
        }
    }
}
