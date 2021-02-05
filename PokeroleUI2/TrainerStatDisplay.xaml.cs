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
    /// Interaction logic for TrainerStatDisplay.xaml
    /// </summary>
    public partial class TrainerStatDisplay : UserControl, INotifyPropertyChanged
    {
        private MainWindow mainwindow;
        public ActiveDataManager dataManager;

        private TrainerData _trainerData;
        public TrainerData trainerData
        {
            get { return _trainerData; }
            set
            {
                if (_trainerData != value)
                {
                    _trainerData = value;
                    OnPropertyChanged();
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        public List<StatDots> StatDotsList;


        public TrainerStatDisplay()
        {
            mainwindow = (PokeroleUI2.MainWindow)Application.Current.MainWindow;
            dataManager = mainwindow.dataManager;
            DataContext = this;
            InitializeComponent();
            dataManager.TrainerChanged += Trainer_PropertyChanged;
            rankControl.PropertyChanged += Stats_PropertyChanged;


            StatDotsList = new List<StatDots> { STRDots, DEXDots, VITDots, INSDots,
                TOUDots, COODots, BEADots, CLEDots, CUTDots,
                BRAWLDots, THROWDots, EVADEDots, WEAPODots, ALERTDots, ATHLEDots, NATURDots, STEALDots, ALLURDots, ETIQUDots, INTIMDots, PERFODots, CRAFTDots, LOREDots, MEDICDots, SCIENDots};
            foreach (StatDots sd in StatDotsList)
            {
                sd.PropertyChanged += Stats_PropertyChanged;
            }
        }

        void Trainer_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            trainerData = dataManager.ActiveTrainer;

            if (trainerData == null) { Clear(); return; }

            UpdateTrainer();
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
        }

        public void UpdateTrainer() {
            if(trainerData == null) { return; }

            rankControl.Update();

            STRDots.SetStats(trainerData.Attributes.GetStatByTag("Strength"), trainerData.Attributes);
            DEXDots.SetStats(trainerData.Attributes.GetStatByTag("Dexterity"), trainerData.Attributes);
            VITDots.SetStats(trainerData.Attributes.GetStatByTag("Vitality"), trainerData.Attributes);
            INSDots.SetStats(trainerData.Attributes.GetStatByTag("Insight"), trainerData.Attributes);

            TOUDots.SetStats(trainerData.SocialAttributes.GetStatByTag("Tough"), trainerData.SocialAttributes);
            COODots.SetStats(trainerData.SocialAttributes.GetStatByTag("Cool"), trainerData.SocialAttributes);
            BEADots.SetStats(trainerData.SocialAttributes.GetStatByTag("Beauty"), trainerData.SocialAttributes);
            CLEDots.SetStats(trainerData.SocialAttributes.GetStatByTag("Clever"), trainerData.SocialAttributes);
            CUTDots.SetStats(trainerData.SocialAttributes.GetStatByTag("Cute"), trainerData.SocialAttributes);

            BRAWLDots.SetStats(trainerData.Skills.GetStatByTag("Brawl"), trainerData.Skills);
            WEAPODots.SetStats(trainerData.Skills.GetStatByTag("Channel"), trainerData.Skills);
            THROWDots.SetStats(trainerData.Skills.GetStatByTag("Clash"), trainerData.Skills);
            EVADEDots.SetStats(trainerData.Skills.GetStatByTag("Evasion"), trainerData.Skills);
            ALERTDots.SetStats(trainerData.Skills.GetStatByTag("Alert"), trainerData.Skills);
            ATHLEDots.SetStats(trainerData.Skills.GetStatByTag("Athletic"), trainerData.Skills);
            NATURDots.SetStats(trainerData.Skills.GetStatByTag("Nature"), trainerData.Skills);
            STEALDots.SetStats(trainerData.Skills.GetStatByTag("Stealth"), trainerData.Skills);
            ALLURDots.SetStats(trainerData.Skills.GetStatByTag("Allure"), trainerData.Skills);
            ETIQUDots.SetStats(trainerData.Skills.GetStatByTag("Etiquette"), trainerData.Skills);
            INTIMDots.SetStats(trainerData.Skills.GetStatByTag("Intimidate"), trainerData.Skills);
            PERFODots.SetStats(trainerData.Skills.GetStatByTag("Perform"), trainerData.Skills);
            CRAFTDots.SetStats(trainerData.Skills.GetStatByTag("Crafts"), trainerData.Skills);
            LOREDots.SetStats(trainerData.Skills.GetStatByTag("Lore"), trainerData.Skills);
            MEDICDots.SetStats(trainerData.Skills.GetStatByTag("Medicine"), trainerData.Skills);
            SCIENDots.SetStats(trainerData.Skills.GetStatByTag("Science"), trainerData.Skills);

            textAttributePoints.Text = trainerData.Attributes.AvailablePoints.ToString();
            textSocialPoints.Text = trainerData.SocialAttributes.AvailablePoints.ToString();
            textSkillPoints.Text = trainerData.Skills.AvailablePoints.ToString();

            hpControl.Update(trainerData.HP);
            willControl.Update(trainerData.Will);
        }

        public void UpdatePointsDisplay()
        {
            textAttributePoints.Text = trainerData.Attributes.AvailablePoints.ToString();
            textSocialPoints.Text = trainerData.SocialAttributes.AvailablePoints.ToString();
            textSkillPoints.Text = trainerData.Skills.AvailablePoints.ToString();

            hpControl.Update(trainerData.HP);
            willControl.Update(trainerData.Will);


            foreach (StatDots sd in StatDotsList)
            {
                sd.UpdateStats();
            }
        }

        public void UpdateImageDisplay()
        {
        }


        void Stats_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (trainerData == null) { return; }
            trainerData.UpdateDependencies();

            UpdatePointsDisplay();
        }


    }
}
