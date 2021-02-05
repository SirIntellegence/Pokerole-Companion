using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
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

namespace PokeroleUI2.Controls
{
    /// <summary>
    /// Interaction logic for HPControl.xaml
    /// </summary>
    public partial class SimpleStatSliderControl : UserControl, INotifyPropertyChanged
    {
        public string statName { get; set; }

        private PkmnSimpleStat _simpleStat;
        public PkmnSimpleStat SimpleStat
        {
            get { return _simpleStat; }
            set
            {
                _simpleStat = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        public SimpleStatSliderControl()
        {
            this.DataContext = this;
            InitializeComponent();
        }

        public void Update(PkmnSimpleStat ss)
        {
            if(ss == null)
            {
                contentBlock.Tag = "";
                SimpleStatSlider.Value = 0;
                SimpleStatSlider.Maximum = 0;
                return;
            }
            this.SimpleStat = ss;
            contentBlock.Tag = statName + ": " + SimpleStat.Value.ToString();
            SimpleStatSlider.Maximum = ss.Max;
            SimpleStatSlider.Value = ss.Value;
        }

        private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if(SimpleStat == null) { return; }
            SimpleStat.Value = (int)SimpleStatSlider.Value;
            contentBlock.Tag = statName + ": " + SimpleStat.Value.ToString();
        }
    }
}
