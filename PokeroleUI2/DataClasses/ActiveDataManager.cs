using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace PokeroleUI2
{
    public class ActiveDataManager : INotifyPropertyChanged
    {

        private TrainerData _activeTrainer;
        public TrainerData ActiveTrainer
        {
            get { return _activeTrainer; }
            set
            {
                if (_activeTrainer != value)
                {
                    _activeTrainer = value;
                    OnTrainerChanged();
                }
            }
        }
        private PokemonData _activeBox;
        public PokemonData ActiveBox
        {
            get { return _activeBox; }
            set
            {
                if (_activeBox != value)
                {
                    _activeBox = value;
                    OnBoxChanged();
                }
            }
        }

        private DexData _activeDex;
        public DexData ActiveDex
        {
            get { return _activeDex; }
            set
            {
                if (_activeDex != value)
                {
                    _activeDex = value;
                    OnDexChanged();
                }
            }
        }
        private MoveData _activeMoveData;
        public MoveData ActiveMoveData
        {
            get { return _activeMoveData; }
            set
            {
                if (_activeMoveData != value)
                {
                    _activeMoveData = value;
                    OnMoveChanged();
                }
            }
        }

        private AbilityData _activeAbilityData;
        public AbilityData ActiveAbilityData
        {
            get { return _activeAbilityData; }
            set
            {
                if (_activeAbilityData != value)
                {
                    _activeAbilityData = value;
                    OnAbilityChanged();
                }
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public event PropertyChangedEventHandler TrainerChanged;
        private void OnTrainerChanged([CallerMemberName] string propertyName = null)
        {
            TrainerChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public event PropertyChangedEventHandler BoxChanged;
        private void OnBoxChanged([CallerMemberName] string propertyName = null)
        {
            BoxChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public event PropertyChangedEventHandler DexChanged;
        private void OnDexChanged([CallerMemberName] string propertyName = null)
        {
            DexChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public event PropertyChangedEventHandler MoveChanged;
        private void OnMoveChanged([CallerMemberName] string propertyName = null)
        {
            MoveChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public event PropertyChangedEventHandler AbilityChanged;
        private void OnAbilityChanged([CallerMemberName] string propertyName = null)
        {
            AbilityChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


    }
}
