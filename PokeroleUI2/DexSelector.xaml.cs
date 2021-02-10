using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
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
    /// Interaction logic for DexSelector.xaml
    /// </summary>
    public partial class DexSelector : UserControl
    {
        private MainWindow mainwindow;
        public ActiveDataManager dataManager;

        public EventHandler DexSelection;

        ObservableCollection<ListData> Data;
        CollectionViewSource SourceList;
        ICollectionView ItemList;

        public DexSelector()
        {
            mainwindow = (PokeroleUI2.MainWindow)Application.Current.MainWindow;
            dataManager = mainwindow.DataManager;

            InitializeComponent();

            Data = DataSerializer.LoadAllListData();
            SourceList = new CollectionViewSource() { Source = Data };
            ItemList = SourceList.View;
            dexGrid.ItemsSource = ItemList;
        }

        private void DexGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ListData selected = (ListData)dexGrid.SelectedItem;
            if (selected != null)
            {
                dataManager.ActiveDex = DataSerializer.LoadDexData(selected.ID);
            }
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            string f = FilterBox.Text.ToLower();
            string[] options = f.Split(':');
            Predicate<object> Filter;
            f = options[options.Length - 1];
            switch (options[0])
            {
                case "type":
                    Filter = new Predicate<object>(item => ((ListData)item).Type1.ToLower().Contains(f) || ((ListData)item).Type2.ToLower().Contains(f));
                    break;
                case "type1":
                    Filter = new Predicate<object>(item => ((ListData)item).Type1.ToLower().Contains(f));
                    break;
                case "type2":
                    Filter = new Predicate<object>(item => ((ListData)item).Type2.ToLower().Contains(f));
                    break;
                case "rank":
                    Filter = new Predicate<object>(item => ((ListData)item).Rank.ToLower().Contains(f));
                    break;
                case "starter":
                    Filter = new Predicate<object>(item => ((ListData)item).Starter);
                    break;
                default:
                    Filter = new Predicate<object>(item => ((ListData)item).Name.ToLower().Contains(f));
                    break;
            }
    
            ItemList.Filter = Filter;
            dexGrid.ItemsSource = ItemList;
        }
    }
}
