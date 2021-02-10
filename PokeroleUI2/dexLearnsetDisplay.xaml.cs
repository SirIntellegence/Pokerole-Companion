using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
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
    /// Interaction logic for dexLearnsetDisplay.xaml
    /// </summary>
    public partial class dexLearnsetDisplay : UserControl
    {
        private MainWindow mainwindow;
        public ActiveDataManager dataManager;

        public LearnsetData learnData { get; set; }
        public EventHandler MoveSelection;
        ObservableCollection<MoveData> data;

        public dexLearnsetDisplay()
        {
            mainwindow = (PokeroleUI2.MainWindow)Application.Current.MainWindow;
            dataManager = mainwindow.DataManager;

            InitializeComponent();

            dataManager.DexChanged += OnDexChanged;

        }

        void OnDexChanged(object sender, EventArgs e)
        {
            Update(dataManager.ActiveDex.ID);
        }

        public void Update(int ID)
        {
            learnData = new LearnsetData(dataManager.ActiveDex.Learnset);
            data = new ObservableCollection<MoveData>(learnData.learnset);
            MoveGrid.ItemsSource = data;
        }

        private void MoveGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            MoveData md = (MoveData)MoveGrid.SelectedItem;
            dataManager.ActiveDexMoveData = md;
        }

        private void Row_Click(object sender, MouseButtonEventArgs e)
        {
            dataManager.DexMoveAbilityToggle = true;
        }
    }

    }
