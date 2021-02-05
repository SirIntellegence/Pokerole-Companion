using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for BoxSelector.xaml
    /// </summary>
    public partial class BoxSelector : UserControl
    {
        private MainWindow mainwindow;
        public ActiveDataManager dataManager;

        public EventHandler BoxSelection;
        public TrainerData activeTrainer;

        public BoxSelector()
        {
            mainwindow = (PokeroleUI2.MainWindow)Application.Current.MainWindow;
            dataManager = mainwindow.dataManager;

            InitializeComponent();
            dataManager.TrainerChanged += OnTrainerChanged;
        }

        void OnTrainerChanged(object sender, EventArgs e)
        {
            activeTrainer = dataManager.ActiveTrainer;
            Update();
        }

        public void Update()
        {
            boxGrid.ItemsSource = activeTrainer.Party;
        }

        private void BoxGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            PokemonData pd = (PokemonData)boxGrid.SelectedItem;
            if(pd != null)
            {
                dataManager.ActiveBox = (PokemonData)boxGrid.SelectedItem;
            }
        }

        
    }
}
