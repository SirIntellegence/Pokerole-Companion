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
    /// Interaction logic for dexStatDisplay.xaml
    /// </summary>
    public partial class dexStatDisplay : UserControl, INotifyPropertyChanged
    {
        private MainWindow mainwindow;
        public ActiveDataManager dataManager;
        private DexData _dexData;
        public DexData dexData
        {
            get { return _dexData; }
            set
            {
                if (_dexData != value)
                {
                    _dexData = value;
                    OnPropertyChanged();
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        public dexStatDisplay()
        {
            mainwindow = (PokeroleUI2.MainWindow)Application.Current.MainWindow;
            dataManager = mainwindow.DataManager;

            DataContext = this;
            InitializeComponent();

            dataManager.DexChanged += OnDexChanged;
        }

        void OnDexChanged(object sender, EventArgs e)
        {
            dexData = dataManager.ActiveDex;
            Update(dexData);
        }

        public void Update(DexData dd)
        {
            dexData = dd;
            STRDots.SetStats(dexData.Attributes.GetStatByTag("Strength"), false);
            DEXDots.SetStats(dexData.Attributes.GetStatByTag("Dexterity"), false);
            VITDots.SetStats(dexData.Attributes.GetStatByTag("Vitality"), false);
            SPEDots.SetStats(dexData.Attributes.GetStatByTag("Special"), false);
            INSDots.SetStats(dexData.Attributes.GetStatByTag("Insight"), false);
            ImageDisplay.Source = PokemonUtils.GetPkmnImage(dexData.ImagePath);
        }
    }
}
