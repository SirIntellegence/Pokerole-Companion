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

namespace PokeroleUI2.Controls
{
    /// <summary>
    /// Interaction logic for HPControl.xaml
    /// </summary>
    public partial class SimpleStatSliderControl : UserControl
    {
        public string statTitle { get; set; }
        public PokemonData pd;
        public PkmnSimpleStat ss;
        public SimpleStatSliderControl()
        {
            this.DataContext = this;
            InitializeComponent();
        }

        public void Update(PokemonData pd, PkmnSimpleStat ss)
        {
            textTitle.Text = statTitle;
            this.pd = pd;
            this.ss = ss;
            Update();
        }

        public void Update()
        {
            SimpleStatSlider.Minimum = 0;
            SimpleStatSlider.Maximum = ss.Max;
            textValue.Text = ss.Value.ToString();
            SimpleStatSlider.Value = ss.Value;
        }

        private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            ss.Value = (int)SimpleStatSlider.Value;
            textValue.Text = ss.Value.ToString();
        }
    }
}
