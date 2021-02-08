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

        public PokemonData pokemonData
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
            pokemonData = dataManager.ActiveBox;
            if(pokemonData == null) {
                Clear();
                return; }
            UpdatePokemon();
            mainwindow.SetColours();
        }

        public void Clear()
        {
            foreach (StatDots sd in StatDotsList)
            {
                sd.Clear();
            }
            UpdateImageDisplay();
            textAttributePoints.Text = "";
            textSocialPoints.Text = "";
            textSkillPoints.Text = "";
            hpControl.Update(null);
            willControl.Update(null);
            textDef.GetBindingExpression(TextBlock.TextProperty).UpdateTarget();
            textSdef.GetBindingExpression(TextBlock.TextProperty).UpdateTarget();
        }

        public void UpdatePokemon()
        {
            if(pokemonData == null)
            {
                return;
            }
            STRDots.SetStats(pokemonData.Attributes.GetStatByTag("Strength"), pokemonData.Attributes);
            DEXDots.SetStats(pokemonData.Attributes.GetStatByTag("Dexterity"), pokemonData.Attributes);
            VITDots.SetStats(pokemonData.Attributes.GetStatByTag("Vitality"), pokemonData.Attributes);
            SPEDots.SetStats(pokemonData.Attributes.GetStatByTag("Special"), pokemonData.Attributes);
            INSDots.SetStats(pokemonData.Attributes.GetStatByTag("Insight"), pokemonData.Attributes);

            TOUDots.SetStats(pokemonData.SocialAttributes.GetStatByTag("Tough"), pokemonData.SocialAttributes);
            COODots.SetStats(pokemonData.SocialAttributes.GetStatByTag("Cool"), pokemonData.SocialAttributes);
            BEADots.SetStats(pokemonData.SocialAttributes.GetStatByTag("Beauty"), pokemonData.SocialAttributes);
            CLEDots.SetStats(pokemonData.SocialAttributes.GetStatByTag("Clever"), pokemonData.SocialAttributes);
            CUTDots.SetStats(pokemonData.SocialAttributes.GetStatByTag("Cute"), pokemonData.SocialAttributes);

            BRAWLDots.SetStats(pokemonData.Skills.GetStatByTag("Brawl"), pokemonData.Skills);
            CHANNDots.SetStats(pokemonData.Skills.GetStatByTag("Channel"), pokemonData.Skills);
            CLASHDots.SetStats(pokemonData.Skills.GetStatByTag("Clash"), pokemonData.Skills);
            EVADEDots.SetStats(pokemonData.Skills.GetStatByTag("Evasion"), pokemonData.Skills);
            ALERTDots.SetStats(pokemonData.Skills.GetStatByTag("Alert"), pokemonData.Skills);
            ATHLEDots.SetStats(pokemonData.Skills.GetStatByTag("Athletic"), pokemonData.Skills);
            NATURDots.SetStats(pokemonData.Skills.GetStatByTag("Nature"), pokemonData.Skills);
            STEALDots.SetStats(pokemonData.Skills.GetStatByTag("Stealth"), pokemonData.Skills);
            ALLURDots.SetStats(pokemonData.Skills.GetStatByTag("Allure"), pokemonData.Skills);
            ETIQUDots.SetStats(pokemonData.Skills.GetStatByTag("Etiquette"), pokemonData.Skills);
            INTIMDots.SetStats(pokemonData.Skills.GetStatByTag("Intimidate"), pokemonData.Skills);
            PERFODots.SetStats(pokemonData.Skills.GetStatByTag("Perform"), pokemonData.Skills);

            LOYALDots.SetStats(pokemonData.Loyalty);
            HAPPYDots.SetStats(pokemonData.Happiness);


            rankControl.Update();
            hpControl.Update(pokemonData.HP);
            willControl.Update(pokemonData.Will);

            UpdateImageDisplay();

            textAttributePoints.Text = pokemonData.Attributes.AvailablePoints.ToString();
            textSocialPoints.Text = pokemonData.SocialAttributes.AvailablePoints.ToString();
            textSkillPoints.Text = pokemonData.Skills.AvailablePoints.ToString();
        }

        public void UpdatePointsDisplay()
        {
            textAttributePoints.Text = pokemonData.Attributes.AvailablePoints.ToString();
            textSocialPoints.Text = pokemonData.SocialAttributes.AvailablePoints.ToString();
            textSkillPoints.Text = pokemonData.Skills.AvailablePoints.ToString();

            hpControl.Update(pokemonData.HP);
            willControl.Update(pokemonData.Will);
            textDef.GetBindingExpression(TextBlock.TextProperty).UpdateTarget();
            textSdef.GetBindingExpression(TextBlock.TextProperty).UpdateTarget();

            foreach (StatDots sd in StatDotsList)
            {
                sd.UpdateStats();
            }
        }

        public void UpdateImageDisplay()
        {
            if(pokemonData == null)
            {
                ImageDisplay.Source = null;
                return;
            }
            ImageDisplay.Source = PokemonUtils.GetPkmnImage(pokemonData.DexData.ImagePath);
    }


        void Stats_PropertyChanged (object sender, PropertyChangedEventArgs e)
        {
            if(pokemonData == null) { return; }
            pokemonData.UpdateDependencies();

            UpdatePointsDisplay();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        
    }
}
