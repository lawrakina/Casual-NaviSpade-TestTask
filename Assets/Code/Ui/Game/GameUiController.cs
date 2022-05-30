using Code.Input;
using UnityEngine;


namespace Code.Ui.Game{
    internal class GameUiController : BaseUiController{
        private readonly PlayerModel _playerModel;
        private readonly GameView _uiView;

        public GameUiController(Transform placeForUi, PlayerModel playerModel, GameView uiView){
            _playerModel = playerModel;
            _uiView = Object.Instantiate(uiView, placeForUi);

            OnShow += DoOnShow;
            OnHide += DoOnHide;
            _playerModel.OnChangeHp += OnChangeHp;
            _playerModel.OnGetBonus += OnGetBonus;

            DoOnHide();
            
            _uiView.ChangeHp(_playerModel.HealthPoints);
            _uiView.ChangeScore(_playerModel.Bonuses);
        }

        private void OnGetBonus(int obj){
            _uiView.ChangeScore(obj);
        }

        private void DoOnHide(){
            _uiView.gameObject.SetActive(false);
        }

        private void OnChangeHp(int obj){
            _uiView.ChangeHp(obj);
        }

        private void DoOnShow(){
            _uiView.gameObject.SetActive(true);
        }

        ~GameUiController(){
            OnShow -= DoOnShow;
        }
    }
}