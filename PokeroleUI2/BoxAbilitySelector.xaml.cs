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
    /// Interaction logic for BoxAbilitySelector.xaml
    /// </summary>
    public partial class BoxAbilitySelector : UserControl
    {
        private MainWindow mainwindow;
        public ActiveDataManager dataManager;

        public List<AbilityData> abilities;
        public PokemonData pd;


        public BoxAbilitySelector()
        {
            mainwindow = (PokeroleUI2.MainWindow)Application.Current.MainWindow;
            dataManager = mainwindow.DataManager;
            InitializeComponent();
            dataManager.BoxChanged += OnBoxChanged;
        }

        void OnBoxChanged(object sender, EventArgs e)
        {
            
            pd = dataManager.ActiveBox;
            if (pd == null) {
                AbilityGrid.ItemsSource = null;
                AbilityGrid.Items.Refresh();
                return; }
            abilities = new List<AbilityData>();
            foreach (string s in pd.Abilities)
            {
                AbilityData a = DataSerializer.LoadAbilityData(s);
                if (a != null)
                {
                    abilities.Add(a);
                }
            }
            Update();
            
        }

        public void Update()
        {
            AbilityGrid.SelectedIndex = pd.CurrentAbilityIndex;
            AbilityGrid.ItemsSource = abilities;
        }

        private void Ability_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;

            AbilityData ad = (AbilityData)button?.DataContext;
            dataManager.ActiveBoxAbility = ad;
            dataManager.BoxMoveAbilityToggle = false;
        }
    }
}
