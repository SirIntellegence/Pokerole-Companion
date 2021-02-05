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

namespace PokeroleUI2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public ActiveDataManager dataManager;

        public MainWindow()
        {
            dataManager = new ActiveDataManager();
            InitializeComponent();

            dataManager.ActiveTrainer = new TrainerData("Red");
            dataManager.DexMoveChanged += UpdateDexMove;
            dataManager.BoxMoveChanged += UpdateBoxMove;
            dataManager.DexAbilityChanged += UpdateDexAbility;
            dataManager.BoxAbilityChanged += UpdateBoxAbility;
            dataManager.DexMoveAbilityToggled += UpdateDexAbility;
            dataManager.BoxMoveAbilityToggled += UpdateBoxAbility;


        }

        private void Catch_Click(object sender, RoutedEventArgs e)
        {
            if(dexStatDisplay.dexData != null)
            {
                dataManager.ActiveTrainer.CatchPokemon(dexStatDisplay.dexData);
            }
        }

        public void UpdateDexMove(object sender, PropertyChangedEventArgs e)
        {
            DexFooterMoveDisplay.SetMove(dataManager.ActiveDexMoveData);
            DexFooterAbilityDisplay.ToggleVisibility(dataManager.DexMoveAbilityToggle);
            DexFooterMoveDisplay.ToggleVisibility(dataManager.DexMoveAbilityToggle);
        }

        public void UpdateBoxMove(object sender, PropertyChangedEventArgs e)
        {
            BoxFooterMoveDisplay.SetMove(dataManager.ActiveBoxMoveData);
            BoxFooterAbilityDisplay.ToggleVisibility(dataManager.BoxMoveAbilityToggle);
            BoxFooterMoveDisplay.ToggleVisibility(dataManager.BoxMoveAbilityToggle);

        }

        public void UpdateDexAbility(object sender, PropertyChangedEventArgs e)
        {
            DexFooterAbilityDisplay.SetAbility(dataManager.ActiveDexAbility);
            DexFooterAbilityDisplay.ToggleVisibility(dataManager.DexMoveAbilityToggle);
            DexFooterMoveDisplay.ToggleVisibility(dataManager.DexMoveAbilityToggle);
        }

        public void UpdateBoxAbility(object sender, PropertyChangedEventArgs e)
        {
            BoxFooterAbilityDisplay.SetAbility(dataManager.ActiveBoxAbility);
            BoxFooterAbilityDisplay.ToggleVisibility(dataManager.BoxMoveAbilityToggle);
            BoxFooterMoveDisplay.ToggleVisibility(dataManager.BoxMoveAbilityToggle);
        }

        public void SetColours()
        {
            if (dexTab.IsSelected == true)
            {
                PokemonUtils.SetTypeColours(dataManager.ActiveDex.Type1, dataManager.ActiveDex.Type2);
            }
            if (boxTab.IsSelected == true)
            {
                PokemonUtils.SetTypeColours(dataManager.ActiveBox.Type1, dataManager.ActiveBox.Type2);
            }
        }
    }
}
