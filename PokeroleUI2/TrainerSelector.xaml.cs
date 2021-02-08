using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
    /// Interaction logic for TrainerSelector.xaml
    /// </summary>
    public partial class TrainerSelector : UserControl
    {
        private MainWindow mainwindow;
        public ActiveDataManager dataManager;

        ObservableCollection<TrainerContainer> data;

        public TrainerSelector()
        {
            mainwindow = (PokeroleUI2.MainWindow)Application.Current.MainWindow;
            dataManager = mainwindow.dataManager;

            InitializeComponent();

            data = DataSerializer.LoadAllTrainerContainers();
            if (data == null) {
                data = new ObservableCollection<TrainerContainer>();
            }

            trainerGrid.ItemsSource = data;
        }

        private void TrainerGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(dataManager.ActiveTrainer != null)
            {
                dataManager.ActiveTrainer.Save();
            }
            foreach(TrainerContainer tc in data)
            {
                tc.PropertyChanged -= OnContainerChanged;
            }

            TrainerContainer selected = (TrainerContainer)trainerGrid.SelectedItem;
            if(selected == null) { dataManager.ActiveBox = null; dataManager.ActiveTrainer = null; return; }
            selected.PropertyChanged += OnContainerChanged;
            TrainerData trainer = selected.LoadTrainer();
            trainer.Container = selected;
            dataManager.ActiveTrainer = trainer;
            dataManager.ActiveBox = null;
        }

        private void AddTrainer_Click(object sender, RoutedEventArgs e)
        {
            TrainerData trainer = new TrainerData("New Trainer");
            data.Add(new TrainerContainer(trainer));
            trainer.Save();
            DataSerializer.SaveTrainerContainers(data);
        }

        private void DeleteTrainer_Click(object sender, RoutedEventArgs e)
        {
            data.Remove((TrainerContainer)trainerGrid.SelectedItem);
            DataSerializer.SaveTrainerContainers(data);

        }

        public void OnContainerChanged(object sender, PropertyChangedEventArgs e)
        {
            DataSerializer.SaveTrainerContainers(data);
        }

    }
}
