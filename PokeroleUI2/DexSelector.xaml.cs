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
    /// Interaction logic for DexSelector.xaml
    /// </summary>
    public partial class DexSelector : UserControl
    {
        private MainWindow mainwindow;
        public ActiveDataManager dataManager;

        public EventHandler DexSelection;
        ObservableCollection<ListData> data;

        public DexSelector()
        {
            mainwindow = (PokeroleUI2.MainWindow)Application.Current.MainWindow;
            dataManager = mainwindow.dataManager;

            InitializeComponent();

            data = DataSerializer.LoadAllListData();
            dexGrid.ItemsSource = data;
        }

        private void DexGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ListData selected = (ListData)dexGrid.SelectedItem;
            dataManager.ActiveDex = DataSerializer.LoadDexData(selected.ID);
        }
        /*
        private void CSVSerialize_Click(object sender, RoutedEventArgs e)
        {
            List<DexData> AllData = new List<DexData>();
            foreach(ListData ld in data)
            {
                AllData.Add(new DexData(ld));
            }
            //DataSerializer.SaveDexData("../DataSources/dex.csv", AllData);
        }

        private void CSVDeserialize_Click(object sender, RoutedEventArgs e)
        {
            data = DataSerializer.LoadListData("../DataSources/dex.csv");
            dexGrid.ItemsSource = data;
        }
        */

    }
}
