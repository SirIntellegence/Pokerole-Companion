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
    /// Interaction logic for StatDots.xaml
    /// </summary>
    public partial class StatDots : UserControl, INotifyPropertyChanged
    {
        public PkmnStatCollection StatParent = null;
        private bool Initialising = false;
        private PkmnStat _stat { get; set; }
        public PkmnStat Stat
        {
            get { return _stat; }
            set
            {
                _stat = value;
                if (!Initialising)
                {
                    OnPropertyChanged();
                }
            }
        }



        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public bool Adjustable = false;
        public double DotHeight = 11;
        public Brush brushBlack = (Brush)Application.Current.Resources["Col_LightBlack"];
        public Brush brushWhite = Brushes.White;
        private List<Ellipse> whitedots;
        private List<Ellipse> blackdots;

        public StatDots()
        {
            InitializeComponent();
            if (System.ComponentModel.DesignerProperties.GetIsInDesignMode(this))
                return;
            this.DataContext = this;
            MakeDots();
        }

        private void MakeDots()
        {
            blackdots = new List<Ellipse>();
            whitedots = new List<Ellipse>();
            for(int i = 0; i < 12; i++) {
                Ellipse dot = new Ellipse() { Width = DotHeight, Height = DotHeight, Fill = brushBlack, Visibility = Visibility.Collapsed };
                wrapPanel.Children.Add(dot);
                blackdots.Add(dot);
            }
            for (int i = 0; i < 12; i++)
            {
                Ellipse dot = new Ellipse() { Width = DotHeight, Height = DotHeight, Fill = brushWhite, Visibility = Visibility.Collapsed };
                wrapPanel.Children.Add(dot);
                whitedots.Add(dot);
            }
        }

        public void Clear()
        {
            Stat = null;
            foreach (Ellipse e in wrapPanel.Children)
            {
                e.Visibility = Visibility.Collapsed;
            }
        }

        private bool AdjustUp()
        {
            if (Stat == null) { return false; }

            bool adjustUp = Adjustable && Stat.CanIncrease;

            if (StatParent != null)
            {
                adjustUp = adjustUp && StatParent.CanSpendPoints;
            }
            return adjustUp;
        }

        private bool AdjustDown()
        {
            if (Stat == null) { return false; }

            bool adjustDown = Adjustable && Stat.CanDecrease;

            return adjustDown;
        }


        public void SetStats(PkmnStat pstat, PkmnStatCollection parent, bool adjustable = true)
        {
            this.StatParent = parent;
            SetStats(pstat, adjustable);
        }

        public void SetStats(PkmnStat pstat, bool adjustable = true)
        {
            if(pstat == null) { Clear(); return; }
            Initialising = true;
            Adjustable = adjustable;
            Stat = pstat;
            UpdateStats();
            Initialising = false;
        }

        public void UpdateStats()
        {
            if(Stat == null)
            {
                return;
            }

            int val = Stat.Value;
            int max = Stat.MaxValue;
            
            for (int i = 0, j = 0; j < 12; i++)
            {
                if (i < val)
                {
                    blackdots[i].Visibility = Visibility.Visible;
                } else if (i < max)
                {
                    blackdots[i].Visibility = Visibility.Collapsed;
                    whitedots[j].Visibility = Visibility.Visible;
                    j++;
                }
                else
                {
                    if(i < 12) {
                    blackdots[i].Visibility = Visibility.Collapsed;
                    }
                    whitedots[j].Visibility = Visibility.Collapsed;
                    j++;
                }
            }
            

            if (!AdjustUp()) { increaseButton.Visibility = Visibility.Collapsed; }
            else { increaseButton.Visibility = Visibility.Visible; }

            if (!AdjustDown()) { decreaseButton.Visibility = Visibility.Collapsed; }
            else { decreaseButton.Visibility = Visibility.Visible; }
        }

        private void IncreaseButton_Click(object sender, RoutedEventArgs e)
        {
            if (Stat == null) { return; }

            if (AdjustUp() == false)
            {
                return;
            }
            if (StatParent != null)
            {
                StatParent.AdjustStat(Stat, 1);
                OnPropertyChanged();
                return;
            }
            Stat.Adjust(1);
            OnPropertyChanged();
        }

        private void DecreaseButton_Click(object sender, RoutedEventArgs e)
        {
            if (Stat == null) { return; }

            if (AdjustDown() == false)
            {
                return;
            }

            if (StatParent != null)
            {
                StatParent.AdjustStat(Stat, -1);
                OnPropertyChanged();
                return;
            }

            Stat.Adjust(-1);
            OnPropertyChanged();
        }
    }
}
