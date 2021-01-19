using System;
using System.Collections.Generic;
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
    /// Interaction logic for boxStatDisplay.xaml
    /// </summary>
    public partial class boxStatDisplay : UserControl, INotifyPropertyChanged
    {
        private MainWindow mainwindow;
        public ActiveDataManager dataManager;
        private PokemonData _pokemonData;
        public PokemonData PokemonData
        {
            get { return _pokemonData; }
            set
            {
                if (_pokemonData != value)
                {
                    _pokemonData = value;
                    OnPropertyChanged();
                }
            }
        }

        public List<StatDots> StatDotsList;
        
        public boxStatDisplay()
        {
            mainwindow = (PokeroleUI2.MainWindow)Application.Current.MainWindow;
            dataManager = mainwindow.dataManager;

            DataContext = this;
            InitializeComponent();
            MainGrid.Visibility = Visibility.Hidden;

            StatDotsList = new List<StatDots> { STRDots, DEXDots, VITDots, SPEDots, INSDots,
                TOUDots, COODots, BEADots, CLEDots, CUTDots,
                BRAWLDots, CHANNDots, EVADEDots, CLASHDots, ALERTDots, ATHLEDots, NATURDots, STEALDots, ALLURDots, ETIQUDots, INTIMDots, PERFODots, LOYALDots, HAPPYDots};
            foreach(StatDots sd in StatDotsList)
            {
                sd.PropertyChanged += Stats_PropertyChanged;
            }

            rankControl.PropertyChanged += Stats_PropertyChanged;
            dataManager.BoxChanged += OnBoxChanged;

        }

        void OnBoxChanged(object sender, EventArgs e)
        {
            PokemonData = dataManager.ActiveBox;
            DisplayNewPokemon(PokemonData);
            mainwindow.SetColours();
        }

        public void DisplayNewPokemon(PokemonData pokemonData)
        {
            PokemonData = pokemonData;
            controlBanner.textName.Text = PokemonData.Name;
            controlBanner.textType1.Text = PokemonData.Type1;
            controlBanner.textType2.Text = PokemonData.Type2;

            STRDots.SetStats(PokemonData.Attributes.GetStatByTag("Strength"), PokemonData.Attributes);
            DEXDots.SetStats(PokemonData.Attributes.GetStatByTag("Dexterity"), PokemonData.Attributes);
            VITDots.SetStats(PokemonData.Attributes.GetStatByTag("Vitality"), PokemonData.Attributes);
            SPEDots.SetStats(PokemonData.Attributes.GetStatByTag("Special"), PokemonData.Attributes);
            INSDots.SetStats(PokemonData.Attributes.GetStatByTag("Insight"), PokemonData.Attributes);

            TOUDots.SetStats(PokemonData.SocialAttributes.GetStatByTag("Tough"), PokemonData.SocialAttributes);
            COODots.SetStats(PokemonData.SocialAttributes.GetStatByTag("Cool"), PokemonData.SocialAttributes);
            BEADots.SetStats(PokemonData.SocialAttributes.GetStatByTag("Beauty"), PokemonData.SocialAttributes);
            CLEDots.SetStats(PokemonData.SocialAttributes.GetStatByTag("Clever"), PokemonData.SocialAttributes);
            CUTDots.SetStats(PokemonData.SocialAttributes.GetStatByTag("Cute"), PokemonData.SocialAttributes);

            BRAWLDots.SetStats(PokemonData.Skills.GetStatByTag("Brawl"), PokemonData.Skills);
            CHANNDots.SetStats(PokemonData.Skills.GetStatByTag("Channel"), PokemonData.Skills);
            CLASHDots.SetStats(PokemonData.Skills.GetStatByTag("Clash"), PokemonData.Skills);
            EVADEDots.SetStats(PokemonData.Skills.GetStatByTag("Evasion"), PokemonData.Skills);
            ALERTDots.SetStats(PokemonData.Skills.GetStatByTag("Alert"), PokemonData.Skills);
            ATHLEDots.SetStats(PokemonData.Skills.GetStatByTag("Athletic"), PokemonData.Skills);
            NATURDots.SetStats(PokemonData.Skills.GetStatByTag("Nature"), PokemonData.Skills);
            STEALDots.SetStats(PokemonData.Skills.GetStatByTag("Stealth"), PokemonData.Skills);
            ALLURDots.SetStats(PokemonData.Skills.GetStatByTag("Allure"), PokemonData.Skills);
            ETIQUDots.SetStats(PokemonData.Skills.GetStatByTag("Etiquette"), PokemonData.Skills);
            INTIMDots.SetStats(PokemonData.Skills.GetStatByTag("Intimidate"), PokemonData.Skills);
            PERFODots.SetStats(PokemonData.Skills.GetStatByTag("Perform"), PokemonData.Skills);

            LOYALDots.SetStats(PokemonData.Loyalty);
            HAPPYDots.SetStats(PokemonData.Happiness);

            textHeight.Text = PokemonData.Height.ToString();
            textWeight.Text = PokemonData.Weight.ToString();

            rankControl.Update(PokemonData);
            hpControl.Update(PokemonData, PokemonData.HP);
            willControl.Update(PokemonData, PokemonData.Will);

            NatureControl.Update(PokemonData.Nature);
            ConfidenceControl.Update(PokemonData.Confidence);
            ItemControl.Update(PokemonData.Item);
            AccessoryControl.Update(PokemonData.Accessory);
            RibbonsControl.Update(PokemonData.Ribbons);
            StatusControl.Update(PokemonData.Status);

            UpdateImageDisplay();

            MainGrid.Visibility = Visibility.Visible;
        }

        public void UpdatePointsDisplay()
        {
            textAttributePoints.Text = PokemonData.Attributes.AvailablePoints.ToString();
            textSocialPoints.Text = PokemonData.SocialAttributes.AvailablePoints.ToString();
            textSkillPoints.Text = PokemonData.Skills.AvailablePoints.ToString();

            foreach (StatDots sd in StatDotsList)
            {
                sd.UpdateStats();
            }
        }

        public void UpdateImageDisplay()
        {
            string path = "pack://application:,,,/Graphics/Sprites/FullRes/" + PokemonUtils.GetImagePath(PokemonData.dexID);
            Uri uri = new Uri(path);
            BitmapImage image = new BitmapImage(uri);
            ImageDisplay.Source = image;
    }


        void Stats_PropertyChanged (object sender, PropertyChangedEventArgs e)
        {
            UpdatePointsDisplay();
            PokemonData.UpdateDependencies();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        
    }
}
