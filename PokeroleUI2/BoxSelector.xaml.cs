using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for BoxSelector.xaml
    /// </summary>
    public partial class BoxSelector : UserControl
    {
        public EventHandler BoxSelection;
        public TrainerData activeTrainer;
        ObservableCollection<PokemonData> data;

        public BoxSelector()
        {
            InitializeComponent();
        }

        public void Update(TrainerData at)
        {
            activeTrainer = at;
            Update();
        }

        public void Update()
        {
            data = new ObservableCollection<PokemonData>(activeTrainer.Party);
            boxGrid.ItemsSource = data;
        }

        private void BoxGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            RaiseBoxSelection((PokemonData)boxGrid.SelectedItem);
        }
        
        private void RaiseBoxSelection(PokemonData selection)
        {
            if (this.BoxSelection != null)
            {
                this.BoxSelection(this, new BoxSelectionArgs(selection));
            }
        }
        
    }


    public class BoxSelectionArgs : EventArgs
    {
        private readonly PokemonData _boxData;
        public PokemonData boxData { get { return _boxData; } }

        public BoxSelectionArgs(PokemonData pd)
        {
            _boxData = pd;
        }
    }
}
