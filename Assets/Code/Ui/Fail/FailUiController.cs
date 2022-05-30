using Code.Data;
using Code.Input;
using UnityEngine;


namespace Code.Ui.Fail{
    internal class FailUiController : BaseUiController{
        private readonly Transform _placeForUi;
        private readonly GameProcessModel _gameModel;
        private readonly PlayerModel _playerModel;
        private readonly FailView _failView;

        public FailUiController(Transform placeForUi, GameProcessModel gameModel, PlayerModel playerModel,
            FailView failView){
            _placeForUi = placeForUi;
            _gameModel = gameModel;
            _playerModel = playerModel;
            _failView = failView;
            _failView = Object.Instantiate(failView, placeForUi);
            
            OnShow+= DoOnShow;
            OnHide += DoOnHide;

            _failView.Init(NewGame);
            
            DoOnHide();
        }

        private void NewGame(){
            _gameModel.OnChangeGameState.Invoke(GameState.StartWindow);
        }

        private void DoOnHide(){
            _failView.gameObject.SetActive(false);
        }

        private void DoOnShow(){
            _failView.SetScore(_playerModel.Bonuses);
            _failView.gameObject.SetActive(true);
        }
    }
}