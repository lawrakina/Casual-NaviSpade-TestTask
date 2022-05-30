using Code.Data;
using Code.Input;
using UnityEngine;


namespace Code.Ui{
    internal class StartUiController : BaseUiController{
        private readonly Transform _placeForUi;
        private readonly GameProcessModel _gameModel;
        private readonly StartView _startView;

        public StartUiController(Transform placeForUi, GameProcessModel gameModel, StartView startView){
            _placeForUi = placeForUi;
            _gameModel = gameModel;
            _startView = Object.Instantiate(startView, placeForUi);

            OnShow += DoOnShow;
            OnHide += DoOnHide;
            
            _startView.Init(StartBattle);
            
            DoOnHide();
        }

        private void StartBattle(){
            _gameModel.OnChangeGameState.Invoke(GameState.Game);
        }

        private void DoOnHide(){
            _startView.gameObject.SetActive(false);
        }

        private void DoOnShow(){
            _startView.gameObject.SetActive(true);
        }
    }
}