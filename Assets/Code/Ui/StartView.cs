using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;


namespace Code.Ui{
    public class StartView : MonoBehaviour{
        [SerializeField]
        private Button _startButton;

        public void Init(UnityAction start){
            _startButton.onClick.AddListener(start);
        }

        ~StartView(){
            _startButton.onClick.RemoveAllListeners();
        }
    }
}
