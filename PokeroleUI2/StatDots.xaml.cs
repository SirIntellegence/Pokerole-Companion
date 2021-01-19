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

        private PkmnStat _stat { get; set; }
        public PkmnStat Stat
        {
            get { return _stat; }
            set
            {
                _stat = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        public bool Adjustable = false;
        public double DotHeight { get; set; }
        public SolidColorBrush brushBlack = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#3c3c3b"));
        public SolidColorBrush brushWhite = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#ffffff"));
        public BitmapImage mask = new BitmapImage(new Uri("pack://application:,,,/Graphics/Icons/CircleMask.png"));
        ImageBrush maskBrush;

        public StatDots()
        {
            InitializeComponent();
            if (System.ComponentModel.DesignerProperties.GetIsInDesignMode(this))
                return;
            this.DataContext = this;
            MakeBrush();
        }

        public void MakeBrush()
        {
            maskBrush = new ImageBrush(mask);
            maskBrush.AlignmentX = AlignmentX.Left;
            maskBrush.AlignmentY = AlignmentY.Top;
            maskBrush.Stretch = Stretch.UniformToFill;
        }

        private bool AdjustUp()
        {
            bool adjustUp = Adjustable && Stat.CanIncrease;

            if (StatParent != null)
            {
                adjustUp = adjustUp && StatParent.CanSpendPoints;
            }
            Debug.WriteLine("StatDotChange");
            return adjustUp;
        }

        private bool AdjustDown()
        {
            bool adjustDown = Adjustable && Stat.CanDecrease;
            Debug.WriteLine("StatDotChange");

            return adjustDown;
        }


        public void SetStats(PkmnStat pstat, PkmnStatCollection parent, bool adjustable = true)
        {
            this.StatParent = parent;
            SetStats(pstat, adjustable);
        }

        public void SetStats(PkmnStat pstat, bool adjustable = true)
        {
            Adjustable = adjustable;
            Stat = pstat;
            UpdateStats();
        }

        public void UpdateStats()
        {
            if(Stat == null)
            {
                return;
            }

            int val = Stat.Value;
            int max = Stat.MaxValue;
            wrapPanel.Children.Clear();
            for (int i = 0; i < max; i++)
            {
                Brush b = brushWhite;
                if (i < val) { b = brushBlack; }

                Rectangle rect = new Rectangle()
                {
                    Width = DotHeight,
                    Height = DotHeight,
                    Fill = b,
                    OpacityMask = maskBrush,

                };

                wrapPanel.Children.Add(rect);

            }
        }

        private void IncreaseButton_Click(object sender, RoutedEventArgs e)
        {
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
