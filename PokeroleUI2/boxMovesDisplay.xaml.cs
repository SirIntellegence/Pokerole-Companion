using GongSolutions.Wpf.DragDrop;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for boxMovesDisplay.xaml
    /// </summary>
    public partial class boxMovesDisplay : UserControl, INotifyPropertyChanged, IDropTarget
    {
        private MainWindow mainwindow;
        public ActiveDataManager dataManager;

        private PokemonData _activeBox;
        public PokemonData ActiveBox
        {
            get { return _activeBox; }
            set
            {
                if (_activeBox != value)
                {
                    _activeBox = value;
                    OnPropertyChanged();
                }
            }
        }



        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public boxMovesDisplay()
        {
            mainwindow = (PokeroleUI2.MainWindow)Application.Current.MainWindow;
            dataManager = mainwindow.dataManager;

            DataContext = this;
            InitializeComponent();

            dataManager.BoxChanged += OnBoxChanged;
            dataManager.TrainerChanged += OnBoxChanged;

        }

        void OnBoxChanged(object sender, EventArgs e)
        {
            ActiveBox = dataManager.ActiveBox;
            OnPropertyChanged("ActiveBox");
        }

        void IDropTarget.DragOver(IDropInfo dropInfo)
        {
            if (dropInfo.Data is MoveData && dropInfo.TargetItem is MoveData)
            {
                dropInfo.DropTargetAdorner = DropTargetAdorners.Highlight;
                dropInfo.Effects = DragDropEffects.Move;
            }
        }
        void IDropTarget.Drop(IDropInfo dropInfo)
        {
            MoveData move = (MoveData)dropInfo.TargetItem;
            MoveData learn = (MoveData)dropInfo.Data;
            int i = Array.IndexOf(ActiveBox.Moves, move);
            if(i != -1)
            {
                ActiveBox.Moves[i] = learn;
            }
            dataManager.ActiveBox.UpdateDependencies();
            OnPropertyChanged("ActiveBox");
        }

        private void LearnGrid_TargetUpdated(object sender, DataTransferEventArgs e)
        {

        }
    }
}
