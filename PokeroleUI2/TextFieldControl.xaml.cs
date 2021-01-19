using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Diagnostics;

namespace PokeroleUI2.Controls
{
    /// <summary>
    /// Interaction logic for TextFieldControl.xaml
    /// </summary>
    public partial class TextFieldControl : UserControl, INotifyPropertyChanged
    {
        private PkmnStringStat _stringStat { get; set; }
        public PkmnStringStat StringStat
        {
            get { return _stringStat; }
            set
            {
                if (_stringStat != value)
                {
                    _stringStat = value;
                    OnPropertyChanged();
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        
        public TextFieldControl()
        {
            this.DataContext = this;
            InitializeComponent();
        }

        public void Update(PkmnStringStat val)
        {
            StringStat = val;
        }

        
        private void TextBoxValue_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (StringStat != null)
            {
                StringStat.value = textBoxValue.Text;
            }
        }


    }
}
