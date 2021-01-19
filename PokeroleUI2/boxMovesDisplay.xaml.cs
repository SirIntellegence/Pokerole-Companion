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
    /// Interaction logic for boxMovesDisplay.xaml
    /// </summary>
    public partial class boxMovesDisplay : UserControl
    {
        private MainWindow mainwindow;
        public ActiveDataManager dataManager;

        public PokemonData pd;
        public List<MoveShort> moveShorts;
        public boxMovesDisplay()
        {
            mainwindow = (PokeroleUI2.MainWindow)Application.Current.MainWindow;
            dataManager = mainwindow.dataManager;
            InitializeComponent();
            dataManager.BoxChanged += OnBoxChanged;
        }

        void OnBoxChanged(object sender, EventArgs e)
        {
            pd = dataManager.ActiveBox;
            Update();
        }

        public void Update(PokemonData pd)
        {
            this.pd = pd;
            Update();
        }



        public void Update()
        {
            if(pd == null)
            {
                return;
            }
            MovesStack.Children.Clear();
            moveShorts = new List<MoveShort>();
            for(int i = 0; i < pd.Moves.Count; i++)
            {
                MovesStack.Children.Add(new MoveShort(i, pd));
            }            
        }
    }
}
