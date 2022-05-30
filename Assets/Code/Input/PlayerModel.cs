using System;
using UnityEngine;


namespace Code.Input{
    [CreateAssetMenu(fileName = nameof(PlayerModel), menuName = "Models/" + nameof(PlayerModel))]
    public class PlayerModel : ScriptableObject{
        private int _healthPoints;
        public Action<int> OnChangeHp = delegate(int i){ };
        public Action<int> OnGetBonus = delegate(int i){ };
        public Action OnDied = delegate{ };
        private int _bonuses;
        public int MaxHealthPoints{ get; private set; }
        public int HealthPoints{
            get => _healthPoints;
            set{
                _healthPoints = value;
                if (value >= MaxHealthPoints){
                    _healthPoints = MaxHealthPoints;
                }

                if (value <= 0){
                    OnDied?.Invoke();
                }

                OnChangeHp?.Invoke(value);
            }
        }
        public int Bonuses{
            get => _bonuses;
            set{
                _bonuses = value;
                OnGetBonus?.Invoke(value);
            }
        }

        public void Init(int maxHp){
            MaxHealthPoints = maxHp;
            HealthPoints = maxHp;
            _bonuses = 0;
        }
    }
}