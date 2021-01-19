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

namespace PokeroleUI2.Controls
{
    /// <summary>
    /// Interaction logic for dexStatDisplay.xaml
    /// </summary>
    public partial class dexStatDisplay : UserControl
    {
        private MainWindow mainwindow;
        public ActiveDataManager dataManager;
        public DexData dexData { get; set;  }

        public dexStatDisplay()
        {
            mainwindow = (PokeroleUI2.MainWindow)Application.Current.MainWindow;
            dataManager = mainwindow.dataManager;

            DataContext = this;
            InitializeComponent();

            dataManager.DexChanged += OnDexChanged;
        }

        void OnDexChanged(object sender, EventArgs e)
        {
            dexData = dataManager.ActiveDex;
            Update(dexData);
            mainwindow.SetColours();
        }

        public void Update(DexData dd)
        {
            dexData = dd;
            controlBanner.textName.Text = dd.Name;
            controlBanner.textType1.Text = dd.Type1;
            controlBanner.textType2.Text = dd.Type2;

            textDesc.Text = "descUpdate";
            textHP.Text = dd.BaseHP.ToString();
            textHeight.Text = dd.Height.ToString();
            textWeight.Text = dd.Weight.ToString();
            textRank.Text = dd.Rank;
            textAbilities.Text = "AbilitiesUpdate";
            textEvo.Text = "EvoUpdate";
            STRDots.SetStats(dexData.Attributes.GetStatByTag("Strength"), false);
            DEXDots.SetStats(dexData.Attributes.GetStatByTag("Dexterity"), false);
            VITDots.SetStats(dexData.Attributes.GetStatByTag("Vitality"), false);
            SPEDots.SetStats(dexData.Attributes.GetStatByTag("Special"), false);
            INSDots.SetStats(dexData.Attributes.GetStatByTag("Insight"), false);
            UpdateImageDisplay();
        }

        public void UpdateImageDisplay()
        {
            string path = "pack://application:,,,/Graphics/Sprites/FullRes/" + PokemonUtils.GetImagePath(dexData.DexID);
            Uri uri = new Uri(path);
            BitmapImage image = new BitmapImage(uri);
            ImageDisplay.Source = image;
        }
    }
}
