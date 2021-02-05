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
    /// Interaction logic for FooterAbilityDisplay.xaml
    /// </summary>
    public partial class FooterAbilityDisplay : UserControl, INotifyPropertyChanged
    {
        private AbilityData _abilityData;
        public AbilityData abilityData
        {
            get { return _abilityData; }
            set
            {
                if (_abilityData != value)
                {
                    _abilityData = value;
                    OnPropertyChanged();
                }
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public FooterAbilityDisplay()
        {
            DataContext = this;
            InitializeComponent();
        }


        public void SetAbility(AbilityData ad)
        {
            if (ad != null)
            {
                this.abilityData = ad;
            }
        }

        public void ToggleVisibility(bool vis)
        {
            if (!vis) { Visibility = Visibility.Visible; }
            else { Visibility = Visibility.Collapsed; }
        }
    }
}
