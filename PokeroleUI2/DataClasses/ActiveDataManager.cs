using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
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
        private MoveData _activeDexMoveData;
        public MoveData ActiveDexMoveData
        {
            get { return _activeDexMoveData; }
            set
            {
                DexMoveAbilityToggle = true;
                if (_activeDexMoveData != value)
                {
                    _activeDexMoveData = value;
                    OnDexMoveChanged();
                }
            }
        }

        private MoveData _activeBoxMoveData;
        public MoveData ActiveBoxMoveData
        {
            get { return _activeBoxMoveData; }
            set
            {
                BoxMoveAbilityToggle = true;
                if (_activeBoxMoveData != value)
                {
                    _activeBoxMoveData = value;
                    OnBoxMoveChanged();
                }
            }
        }

        private AbilityData _activeDexAbility;
        public AbilityData ActiveDexAbility
        {
            get { return _activeDexAbility; }
            set
            {
                DexMoveAbilityToggle = false;
                if (_activeDexAbility != value)
                {
                    _activeDexAbility = value;
                    OnDexAbilityChanged();
                }
            }
        }

        private AbilityData _activeBoxAbility;
        public AbilityData ActiveBoxAbility
        {
            get { return _activeBoxAbility; }
            set
            {
                BoxMoveAbilityToggle = false;
                if (_activeBoxAbility != value)
                {
                    _activeBoxAbility = value;
                    OnBoxAbilityChanged();
                }
            }
        }

        public bool _boxMoveAbilityToggle;
        public bool BoxMoveAbilityToggle //true is move, false is ability
        {
            get { return _boxMoveAbilityToggle; }
            set
            {
                
                if (_boxMoveAbilityToggle != value)
                {
                    _boxMoveAbilityToggle = value;
                    OnBoxMoveAbilityToggled();
                }
            }
        }

        public bool _dexMoveAbilityToggle;
        public bool DexMoveAbilityToggle //true is move, false is ability
        {
            get { return _dexMoveAbilityToggle; }
            set
            {
                if (_dexMoveAbilityToggle != value)
                {
                    _dexMoveAbilityToggle = value;
                    OnDexMoveAbilityToggled();
                }
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public event PropertyChangedEventHandler DexMoveAbilityToggled;
        private void OnDexMoveAbilityToggled([CallerMemberName] string propertyName = null)
        {
            DexMoveAbilityToggled?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public event PropertyChangedEventHandler BoxMoveAbilityToggled;
        private void OnBoxMoveAbilityToggled([CallerMemberName] string propertyName = null)
        {
            BoxMoveAbilityToggled?.Invoke(this, new PropertyChangedEventArgs(propertyName));
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
        public event PropertyChangedEventHandler DexMoveChanged;
        private void OnDexMoveChanged([CallerMemberName] string propertyName = null)
        {
            DexMoveChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public event PropertyChangedEventHandler BoxMoveChanged;
        private void OnBoxMoveChanged([CallerMemberName] string propertyName = null)
        {
            BoxMoveChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public event PropertyChangedEventHandler DexAbilityChanged;
        private void OnDexAbilityChanged([CallerMemberName] string propertyName = null)
        {
            DexAbilityChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public event PropertyChangedEventHandler BoxAbilityChanged;
        private void OnBoxAbilityChanged([CallerMemberName] string propertyName = null)
        {
            BoxAbilityChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


    }
}