using System.Collections.Generic;
using Code.Data;
using Code.Input;
using Code.Ui.Fail;
using Code.Ui.Game;
using UnityEngine;


namespace Code.Ui{
    internal class UiController{
        private readonly Transform _placeForUi;
        private readonly UiSettings _uiSettings;
        private readonly PlayerModel _playerModel;


        #region privateData

        private GameState _oldGameState = GameState.None;
        private Dictionary<GameState, BaseUiController> _windows = new Dictionary<GameState, BaseUiController>();
        private GameProcessModel _gameModel;

        #endregion


        public UiController(Transform placeForUi, UiSettings uiSettings, GameProcessModel gameModel,
            PlayerModel playerModel){
            _placeForUi = placeForUi;
            _uiSettings = uiSettings;
            _gameModel = gameModel;
            _playerModel = playerModel;
        }

        public void Init(){
            _windows.Add(GameState.StartWindow, new StartUiController(_placeForUi, _gameModel, _uiSettings.StartView));
            _windows.Add(GameState.Game, new GameUiController(_placeForUi, _playerModel, _uiSettings.GameView));
            _windows.Add(GameState.FailWindow, new FailUiController(_placeForUi,_gameModel, _playerModel, _uiSettings.FailView));

            _gameModel.OnChangeGameState += ChangeGameState;
        }

        private void ChangeGameState(GameState state){
            if (_oldGameState == state) return;
            HideAllWindowWithout(state);
            // if(_oldGameState == )
            _oldGameState = state;
        }

        private void HideAllWindowWithout(GameState state){
            foreach (var kvp in _windows){
                if (kvp.Key != state)
                    kvp.Value.Hide();
                else
                    kvp.Value.Show();
            }
        }
    }
}