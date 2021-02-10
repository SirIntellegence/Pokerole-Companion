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
    /// Interaction logic for TrainerBackpack.xaml
    /// </summary>
    public partial class TrainerBackpack : UserControl, INotifyPropertyChanged
    {
        private MainWindow mainwindow;
        public ActiveDataManager dataManager;

        private BackpackData _backpackData;
        public BackpackData backpackData
        {
            get { return _backpackData; }
            set
            {
                if (_backpackData != value)
                {
                    _backpackData = value;
                    OnPropertyChanged();
                }
            }
        }

        public TrainerBackpack()
        {
            mainwindow = (PokeroleUI2.MainWindow)Application.Current.MainWindow;
            dataManager = mainwindow.DataManager;
            InitializeComponent();
            dataManager.TrainerChanged += Trainer_PropertyChanged;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        void Trainer_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if(dataManager.ActiveTrainer != null) {
                backpackData = dataManager.ActiveTrainer.Backpack;
            }
        }
    }
}
