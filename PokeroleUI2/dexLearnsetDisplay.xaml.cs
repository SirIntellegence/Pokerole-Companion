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
            dataManager = mainwindow.dataManager;

            InitializeComponent();

            dataManager.DexChanged += OnDexChanged;

        }

        void OnDexChanged(object sender, EventArgs e)
        {
            Update(dataManager.ActiveDex.ID);
        }

        public void Update(int ID)
        {
            learnData = new LearnsetData(ID);
            data = new ObservableCollection<MoveData>(learnData.learnset);
            MoveGrid.ItemsSource = data;
        }

        private void RaiseMoveSelection(MoveData selection)
        {
            if (this.MoveSelection != null)
            {
                this.MoveSelection(this, new MoveSelectionArgs(selection));
            }
        }

        private void MoveGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            RaiseMoveSelection((MoveData)MoveGrid.SelectedItem);
        }
    }

    public class MoveSelectionArgs : EventArgs
    {
        private readonly MoveData _moveData;
        public MoveData moveData { get { return _moveData; } }

        public MoveSelectionArgs(MoveData md)
        {
            _moveData = md;
        }
    }
    }
