using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
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
    /// Interaction logic for FooterMoveDisplay.xaml
    /// </summary>
    public partial class FooterMoveDisplay : UserControl, INotifyPropertyChanged
    {
        private MoveData _moveData;
        public MoveData moveData
        {
            get { return _moveData; }
            set
            {
                if (_moveData != value)
                {
                    _moveData = value;
                    OnPropertyChanged();
                }
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }



        public FooterMoveDisplay()
        {
            DataContext = this;
            InitializeComponent();
        }

        public void SetMove(MoveData md)
        {
            if (md != null) {
                this.moveData = md;
            }
        }

        public void ToggleVisibility(bool vis)
        {
            if (vis) { Visibility = Visibility.Visible; }
            else { Visibility = Visibility.Collapsed; }
        }
    }
}
