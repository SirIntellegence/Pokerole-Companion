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
        private MainWindow mainwindow;
        public ActiveDataManager dataManager;

        public static readonly DependencyProperty RankProperty = DependencyProperty.Register("Rank", typeof(int), typeof(RankControl));
        public int Rank
        {
            get { return (int)GetValue(RankProperty); }
            set { SetValue(RankProperty, value); }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public RankControl()
        {
            mainwindow = (PokeroleUI2.MainWindow)Application.Current.MainWindow;
            dataManager = mainwindow.DataManager;

            InitializeComponent();
        }

        public void Update()
        {
            textRank.Text = PokemonUtils.RankFromInt(Rank);
            imageRank.Source = new BitmapImage(new Uri("pack://application:,,,/Graphics/Icons/Rank_" + PokemonUtils.RankFromInt(Rank) + ".png"));
        }

        private void DecreaseButton_Click(object sender, RoutedEventArgs e)
        {
            Rank = Math.Max(0, Rank - 1);
            OnPropertyChanged();
            Update();
        }

        private void IncreaseButton_Click(object sender, RoutedEventArgs e)
        {
            Rank = Math.Min(6, Rank + 1);
            OnPropertyChanged();
            Update();
        }
    }
}
