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
    /// Interaction logic for BoxNotes.xaml
    /// </summary>
    public partial class BoxNotes : UserControl, INotifyPropertyChanged
    {
        private MainWindow mainwindow;
        public ActiveDataManager dataManager;

        private PokemonData _pokemonData;
        public PokemonData PokemonData
        {
            get { return _pokemonData; }
            set
            {
                if (_pokemonData != value)
                {
                    _pokemonData = value;
                    OnPropertyChanged();
                }
            }
        }

        public BoxNotes()
        {
            mainwindow = (PokeroleUI2.MainWindow)Application.Current.MainWindow;
            dataManager = mainwindow.DataManager;
            InitializeComponent();
            dataManager.BoxChanged += Box_PropertyChanged;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        void Box_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (dataManager.ActiveBox != null)
            {
                PokemonData = dataManager.ActiveBox;
            }
        }
    }
}
