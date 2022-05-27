using System;
using System.Collections.Generic;
using Code.Data;
using Code.Input;
using UnityEngine;
using Object = UnityEngine.Object;


namespace Code.Ui{
    internal class UiController{
        private readonly Transform _placeForUi;
        private readonly UiSettings _uiSettings;
        private readonly InputModel _inputModel;
        private readonly PlayerModel _playerModel;


        #region privateData

        private GameState _oldGameState = GameState.None;
        private Dictionary<GameState, BaseUiController> _windows;

        #endregion
        
        public UiController(Transform placeForUi, UiSettings uiSettings, InputModel inputModel, PlayerModel playerModel){
            _placeForUi = placeForUi;
            _uiSettings = uiSettings;
            _inputModel = inputModel;
            _playerModel = playerModel;
        }

        public void Init(){
            _windows = new Dictionary<GameState,BaseUiController>();
         
            _windows.Add(GameState.Game, new GameUiController(_placeForUi, _inputModel, _playerModel,_uiSettings.GameView));
            
            _inputModel.OnChangeGameState += ChangeGameState;
        }

        private void ChangeGameState(GameState state){
            if (_oldGameState == state) return;
            HideAllWindowWithout(state);
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

    internal class GameUiController : BaseUiController{
        private readonly InputModel _inputModel;
        private readonly PlayerModel _playerModel;
        private readonly GameView _uiView;

        public GameUiController(Transform placeForUi, InputModel inputModel, PlayerModel playerModel, GameView uiView){
            _inputModel = inputModel;
            _playerModel = playerModel;
            _uiView = Object.Instantiate(uiView, placeForUi);

            OnShow += DoOnShow;
            
            _playerModel.OnChangeHp += OnChangeHp;
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

    internal class BaseUiController{
        
        public event Action OnHide;
        public event Action OnShow;
        public void Hide(){
            OnHide?.Invoke();
            
        }

        public void Show(){
            OnShow?.Invoke();
            
        }
    }
}