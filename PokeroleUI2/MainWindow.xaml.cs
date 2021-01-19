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
            dataManager.ActiveTrainer = new TrainerData("Red");
            InitializeComponent();
            //dexSelector.DexSelection += new EventHandler(dexSelector_DexSelection);
            boxSelector.BoxSelection += new EventHandler(boxSelector_BoxSelection);
        }

        private void dexSelector_DexSelection(object sender, EventArgs e)
        {
            DexSelectionArgs dsa = (DexSelectionArgs)e;
            dataManager.ActiveDex = dsa.dexData;
            //DexUpdate();
        }

        private void DexUpdate()
        {
            if (dataManager.ActiveDex == null)
            {
                //hide dex
                return;
            }
            //show dex
            SetColours();
            dexStatDisplay.Update(dataManager.ActiveDex);
            dexLearnset.Update(dataManager.ActiveDex.ID);
        }

        private void boxSelector_BoxSelection(object sender, EventArgs e)
        {
            BoxSelectionArgs bsa = (BoxSelectionArgs)e;
            dataManager.ActiveBox = bsa.boxData;
            //BoxUpdate();
        }
/*
        private void BoxUpdate()
        {
            if(dataManager.ActiveBox == null)
            {
                //hide box
                return;
            }
            //show box
            SetColours();
            boxStatDisplay.DisplayNewPokemon(dataManager.ActiveBox);
            boxMovesDisplay.Update(dataManager.ActiveBox);
        }
        */

        private void Catch_Click(object sender, RoutedEventArgs e)
        {
            if(dexStatDisplay.dexData != null)
            {
                dataManager.ActiveTrainer.CatchPokemon(dexStatDisplay.dexData);
                boxSelector.Update(dataManager.ActiveTrainer);
                dataManager.ActiveBox = dataManager.ActiveTrainer.ActivePokemon;
                //BoxUpdate();
            }
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
