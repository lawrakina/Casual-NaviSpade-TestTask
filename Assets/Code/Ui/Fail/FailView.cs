using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;


namespace Code.Ui.Fail{
    internal class FailView : BaseView{
        [SerializeField]
        private TextMeshProUGUI _score;
        [SerializeField]
        private Button _startButton;

        public void Init(UnityAction start){
            _startButton.onClick.AddListener(start);
        }

        ~FailView(){
            _startButton.onClick.RemoveAllListeners();
        }

        public void SetScore(int bonuses){
            _score.text = $"{bonuses}";
        }
    }
}