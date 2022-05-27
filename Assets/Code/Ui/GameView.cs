using System;
using TMPro;
using UnityEngine;


namespace Code.Ui{
    internal class GameView: BaseView{
        [SerializeField]
        private TextMeshProUGUI _rounds;
        [SerializeField]
        private TextMeshProUGUI _score;
        [SerializeField]
        private TextMeshProUGUI _lives;
        [SerializeField]
        private TextMeshProUGUI _enemies;

        public void ChangeHp(int i){
            _lives.text = i.ToString();
        }
    }
}