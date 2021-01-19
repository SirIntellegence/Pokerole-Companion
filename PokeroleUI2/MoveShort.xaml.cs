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

        public int slotIndex;
        public PokemonData pd;
        public MoveData md;


        public MoveShort(int i, PokemonData pd)
        {
            InitializeComponent();
            this.pd = pd;
            slotIndex = i;
            Update();
        }

        public MoveShort()
        {
            InitializeComponent();
        }

        public void Update(PokemonData p)
        {
            pd = p;
            Update();
        }

        public void Update()
        {
            md = pd.Moves[slotIndex];

            textName.Text = md.Name;
            textPower.Text = md.Power;
            textType.Text = md.Type;
            textCategory.Text = md.Category;
            int power = 0;
            int.TryParse(md.Power, out power);
            int damagestat = pd.Attributes.GetStatByTag(md.PowerStat).Value + power;
            textDamage.Text = damagestat.ToString();

            int accstat1 = pd.Attributes.GetStatByTag(md.AccuracyStat1).Value;
            int accstat2 = pd.Skills.GetStatByTag(md.AccuracyStat2).Value;

            textAccuracy.Text = (accstat1 + accstat2).ToString();

            moveSelector.ItemsSource = pd.LearnSet.learnset;

        }

        private void MoveSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            pd.Moves[slotIndex] = (MoveData)moveSelector.SelectedItem;
            Update();
        }
    }
}
