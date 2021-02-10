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
    /// Interaction logic for BoxSelector.xaml
    /// </summary>
    public partial class BoxSelector : UserControl, INotifyPropertyChanged, IDropTarget
    {
        private MainWindow mainwindow;
        public ActiveDataManager dataManager;

        public EventHandler BoxSelection;
        private TrainerData _activeTrainer;
        public TrainerData ActiveTrainer
        {
            get { return _activeTrainer; }
            set
            {
                if (_activeTrainer != value)
                {
                    _activeTrainer = value;
                    OnPropertyChanged();
                }
            }
        }




        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public BoxSelector()
        {
            mainwindow = (PokeroleUI2.MainWindow)Application.Current.MainWindow;
            dataManager = mainwindow.DataManager;

            InitializeComponent();
            dataManager.TrainerChanged += OnTrainerChanged;
        }

        void OnTrainerChanged(object sender, EventArgs e)
        {
            ActiveTrainer = dataManager.ActiveTrainer;
            OnPropertyChanged("ActiveTrainer");
            if (ActiveTrainer == null) { return; }
        }


        private void BoxGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
            DataGrid dg = (DataGrid)sender;
            if(dg == boxgrid)
            {
                partygrid.SelectedItem = null;
            }
            if(dg == partygrid)
            {
                boxgrid.SelectedItem = null;
            }
            PokemonData pd = (PokemonData)dg.SelectedItem;
            if(pd != null)
            {
                dataManager.ActiveBox = pd;
            }
        }
        
        void IDropTarget.DragOver(IDropInfo dropInfo)
        {
            PokemonData sourceItem = dropInfo.Data as PokemonData;

            if (sourceItem != null)
            {
                dropInfo.DropTargetAdorner = DropTargetAdorners.Insert;
                dropInfo.Effects = DragDropEffects.Move;
            }
        }
        void IDropTarget.Drop(IDropInfo dropInfo)
        {
            PokemonData source = (PokemonData)dropInfo.Data;
            ObservableCollection<PokemonData> targetcollection = (ObservableCollection<PokemonData>)dropInfo.TargetCollection;
            ObservableCollection<PokemonData> sourcecollection = (ObservableCollection<PokemonData>)dropInfo.DragInfo.SourceCollection;

            sourcecollection.Remove(source);
            if (dropInfo.InsertIndex < targetcollection.Count)
            {
                targetcollection.Insert(dropInfo.InsertIndex, source);
            } else
            {
                targetcollection.Add(source);
            }
            ActiveTrainer.ShiftParty();

            dataManager.ActiveTrainer.UpdateDependencies();
            OnPropertyChanged("ActiveTrainer");
        }
    }


}
