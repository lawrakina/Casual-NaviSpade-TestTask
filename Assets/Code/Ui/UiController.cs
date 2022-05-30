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
        private Dictionary<GameState, BaseUiController> _windows = new Dictionary<GameState, BaseUiController>();
        private GameProcessModel _gameModel;

        #endregion


        public UiController(Transform placeForUi, UiSettings uiSettings, GameProcessModel gameModel,
            InputModel inputModel, PlayerModel playerModel){
            _placeForUi = placeForUi;
            _uiSettings = uiSettings;
            _gameModel = gameModel;
            _inputModel = inputModel;
            _playerModel = playerModel;
        }

        public void Init(){
            _windows.Add(GameState.StartWindow, new StartUiController(_placeForUi, _gameModel, _uiSettings.StartView));
            _windows.Add(GameState.Game, new GameUiController(_placeForUi, _inputModel, _playerModel, _uiSettings.GameView));
            _windows.Add(GameState.FailWindow, new FailUiController(_placeForUi, _playerModel, _uiSettings.FailView));

            _gameModel.OnChangeGameState += ChangeGameState;
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

    internal class FailUiController : BaseUiController{
        private readonly Transform _placeForUi;
        private readonly PlayerModel _playerModel;
        private readonly FailView _failView;

        public FailUiController(Transform placeForUi, PlayerModel playerModel, FailView failView){
            _placeForUi = placeForUi;
            _playerModel = playerModel;
            _failView = failView;
        }
    }

    internal class StartUiController : BaseUiController{
        private readonly Transform _placeForUi;
        private readonly GameProcessModel _gameModel;
        private readonly StartView _startView;

        public StartUiController(Transform placeForUi, GameProcessModel gameModel, StartView startView){
            _placeForUi = placeForUi;
            _gameModel = gameModel;
            _startView = startView;

            OnShow += DoOnShow;
            OnHide += DoOnHide;
            
            _startView.Init(StartBattle);
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