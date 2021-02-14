using System;
using System.Collections.Generic;
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
using PokeroleUI2.Controls;
using System.Diagnostics;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace PokeroleUI2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        private ActiveDataManager _dataManager;
        public ActiveDataManager DataManager
        {
            get { return _dataManager; }
            set
            {
                if (_dataManager != value)
                {
                    _dataManager = value;
                    OnPropertyChanged();
                }
            }
        }



        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }



        public MainWindow()
        {
            DataManager = new ActiveDataManager();
            InitializeComponent();
            DataManager.DexMoveChanged += UpdateDexMove;
            DataManager.BoxMoveChanged += UpdateBoxMove;
            DataManager.DexAbilityChanged += UpdateDexAbility;
            DataManager.BoxAbilityChanged += UpdateBoxAbility;
            DataManager.DexMoveAbilityToggled += UpdateDexAbility;
            DataManager.BoxMoveAbilityToggled += UpdateBoxAbility;
            trainerStatDisplay.PropertyChanged += trainerSelector.OnContainerChanged;

            this.Closed += new EventHandler(MainWindow_Closed);
        }

        private void Catch_Click(object sender, RoutedEventArgs e)
        {
            if((DataManager.ActiveTrainer != null) && (DataManager.ActiveDex != null))
            {
                DataManager.ActiveTrainer.AddPokemon(DataManager.ActiveDex);
            }
        }

        public void UpdateDexMove(object sender, PropertyChangedEventArgs e)
        {
            DexFooterMoveDisplay.SetMove(DataManager.ActiveDexMoveData);
            DexFooterAbilityDisplay.ToggleVisibility(DataManager.DexMoveAbilityToggle);
            DexFooterMoveDisplay.ToggleVisibility(DataManager.DexMoveAbilityToggle);
        }

        public void UpdateBoxMove(object sender, PropertyChangedEventArgs e)
        {
            BoxFooterMoveDisplay.SetMove(DataManager.ActiveBoxMoveData);
            BoxFooterAbilityDisplay.ToggleVisibility(DataManager.BoxMoveAbilityToggle);
            BoxFooterMoveDisplay.ToggleVisibility(DataManager.BoxMoveAbilityToggle);

        }

        public void UpdateDexAbility(object sender, PropertyChangedEventArgs e)
        {
            DexFooterAbilityDisplay.SetAbility(DataManager.ActiveDexAbility);
            DexFooterAbilityDisplay.ToggleVisibility(DataManager.DexMoveAbilityToggle);
            DexFooterMoveDisplay.ToggleVisibility(DataManager.DexMoveAbilityToggle);
        }

        public void UpdateBoxAbility(object sender, PropertyChangedEventArgs e)
        {
            BoxFooterAbilityDisplay.SetAbility(DataManager.ActiveBoxAbility);
            BoxFooterAbilityDisplay.ToggleVisibility(DataManager.BoxMoveAbilityToggle);
            BoxFooterMoveDisplay.ToggleVisibility(DataManager.BoxMoveAbilityToggle);
        }

        void MainWindow_Closed(object sender, EventArgs e)
        {
            DataManager.SaveTrainer(true);
        }
    }
}
