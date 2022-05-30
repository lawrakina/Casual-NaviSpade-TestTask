using TMPro;
using UnityEngine;


namespace Code.Ui.Game{
    internal class GameView: BaseView{
        [SerializeField]
        private TextMeshProUGUI _rounds;
        [SerializeField]
        private TextMeshProUGUI _score;
        [SerializeField]
        private TextMeshProUGUI _lives;
        [SerializeField]
        private TextMeshProUGUI _enemies;
        [SerializeField]
        private TextMeshProUGUI _cristals;
        
        [SerializeField]
        private TextMeshProUGUI _nearEnemy;
        
        [SerializeField]
        private TextMeshProUGUI _nearCristal;
        
        
        public void ChangeHp(int i){
            _lives.text = i.ToString();
        }

        public void ChangeScore(int i){
            _score.text = i.ToString();
        }
    }
}