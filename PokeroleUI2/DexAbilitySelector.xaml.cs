using System;
using System.Collections.Generic;
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
    /// Interaction logic for AbilitySelector.xaml
    /// </summary>
    public partial class DexAbilitySelector : UserControl
    {
        private MainWindow mainwindow;
        public ActiveDataManager dataManager;

        public List<AbilityData> abilities;
        public DexData dd;


        public DexAbilitySelector()
        {
            mainwindow = (PokeroleUI2.MainWindow)Application.Current.MainWindow;
            dataManager = mainwindow.dataManager;
            InitializeComponent();
            dataManager.DexChanged += OnDexChanged;
        }

        void OnDexChanged(object sender, EventArgs e)
        {
            dd = dataManager.ActiveDex;
            abilities = new List<AbilityData>();
            foreach(string s in dd.Abilities)
            {
                abilities.Add(DataSerializer.LoadAbilityData(s));
            }
            Update();
        }

        public void Update()
        {
            AbilityGrid.ItemsSource = abilities;
        }
        private void Ability_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;

            AbilityData ad = (AbilityData)button?.DataContext;
            dataManager.ActiveDexAbility = ad;
            dataManager.DexMoveAbilityToggle = false;
        }
    }
}
