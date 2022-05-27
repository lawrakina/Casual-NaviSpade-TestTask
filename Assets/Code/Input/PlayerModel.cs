﻿using System;
using UnityEngine;


namespace Code.Input{
    [CreateAssetMenu(fileName = nameof(PlayerModel), menuName = "Models/" + nameof(PlayerModel))]
    public class PlayerModel : ScriptableObject{
        private int _healthPoints;
        public Action<int> OnChangeHp = delegate(int i){ };
        public Action OnDied = delegate{  };
        public int MaxHealthPoints{ get; private set; }
        public int HealthPoints{
            get => _healthPoints;
            set{
                _healthPoints = value;
                if (value >= MaxHealthPoints){
                    _healthPoints = MaxHealthPoints;
                }

                OnChangeHp?.Invoke(value);
                if (value <= 0){
                    OnDied?.Invoke();
                }
            }
        }

        public void Init(int maxHp){
            MaxHealthPoints = maxHp;
            HealthPoints = maxHp;
        }
    }
}