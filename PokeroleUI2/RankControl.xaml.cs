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
    /// Interaction logic for RankControl.xaml
    /// </summary>
    public partial class RankControl : UserControl, INotifyPropertyChanged
    {
        public PokemonData pd;



        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public RankControl()
        {
            InitializeComponent();
        }

        public void Update(PokemonData pd)
        {
            this.pd = pd;
            Update();
        }

        public void Update()
        {
            textRank.Text = pd.TextRank;
        }

        private void DecreaseButton_Click(object sender, RoutedEventArgs e)
        {
            pd.Rank = Math.Max(0, pd.Rank - 1);
            pd.UpdateDependencies();
            OnPropertyChanged();
            Update();
        }

        private void IncreaseButton_Click(object sender, RoutedEventArgs e)
        {
            pd.Rank = Math.Min(6, pd.Rank + 1);
            pd.UpdateDependencies();
            OnPropertyChanged();
            Update();
        }
    }
}
