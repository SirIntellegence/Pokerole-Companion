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
    /// Interaction logic for MoveShortDisplay.xaml
    /// </summary>
    /// 


    public partial class MoveShort : UserControl
    {
        private MainWindow mainwindow;
        public ActiveDataManager dataManager;

        public int slotIndex;
        public PokemonData pd { get { return dataManager.ActiveBox; } set { dataManager.ActiveBox = value; } }
        public MoveData md;


        public MoveShort(int i, PokemonData pd)
        {
            mainwindow = (PokeroleUI2.MainWindow)Application.Current.MainWindow;
            dataManager = mainwindow.dataManager;

            InitializeComponent();
            slotIndex = i;
            Update();
        }

        public MoveShort()
        {
            InitializeComponent();
        }

        public void Update()
        {
            md = pd.LearnSet.GetByName(pd.Moves[slotIndex]);

            textName.Text = md.Name;
            textPower.Text = md.Power;
            textType.Text = md.Type;

            Brush typebrush = PokemonUtils.GetTypeColour(md.Type);
            rectBackground.Fill = typebrush;
            imageCat.Source = PokemonUtils.GetCategoryImage(md.Category);

            int power = 0;
            int.TryParse(md.Power, out power);
            int damagestat = pd.Attributes.GetStatByTag(md.PowerStat).Value + power;

            int accstat1 = pd.Attributes.GetStatByTag(md.AccuracyStat1).Value;
            int accstat2 = pd.Skills.GetStatByTag(md.AccuracyStat2).Value;


            moveSelector.ItemsSource = pd.LearnSet.learnset;

        }

        private void MoveSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            MoveData m = (MoveData)moveSelector.SelectedItem;
            if(m == null) { return; } //Apparently this can fucking happen...
            pd.Moves[slotIndex] = m.Name;
            Update();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if(md != null && pd != null) { dataManager.ActiveBoxMoveData = md; }            
        }
    }
}
