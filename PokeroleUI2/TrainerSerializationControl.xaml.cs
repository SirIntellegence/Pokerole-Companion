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
    /// Interaction logic for TrainerSerializationControl.xaml
    /// </summary>
    public partial class TrainerSerializationControl : UserControl
    {
        private MainWindow mainwindow;
        public ActiveDataManager dataManager;

        public TrainerSerializationControl()
        {
            mainwindow = (PokeroleUI2.MainWindow)Application.Current.MainWindow;
            dataManager = mainwindow.dataManager;

            InitializeComponent();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            TrainerData trainer = dataManager.ActiveTrainer;

            DataSerializer.SaveXML(dataManager.ActiveTrainer, trainer.path, typeof(TrainerData));
        }

        private void LoadButton_Click(object sender, RoutedEventArgs e)
        {
            TrainerData trainer = (TrainerData)DataSerializer.LoadXML("../SaveData/trainer.xml", dataManager.ActiveTrainer.GetType());
            dataManager.ActiveBox = null;
            dataManager.ActiveTrainer = trainer;
        }
    }
}
