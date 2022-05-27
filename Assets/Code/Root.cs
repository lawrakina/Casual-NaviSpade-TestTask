using System;
using System.Collections.Generic;
using Code.Bonuses;
using Code.Data;
using Code.Enemies;
using Code.Input;
using Code.Player;
using Code.Ui;
using UnityEngine;


namespace Code{
    public sealed class Root : MonoBehaviour{
        [Header("UI Windows")]
        [SerializeField]
        private UiSettings _uiSettings;
        [SerializeField]
        private Transform _placeForUi;
        [Space]
        [Header("Models")]
        [SerializeField]
        private InputModel _inputModel;
        [SerializeField]
        private PlayerModel _playerModel;
        [Space]
        [Header("Settings")]
        [SerializeField]
        private UnitSettings _unitSettings;
        [SerializeField]
        private LevelSettings _levelSettings;
        [SerializeField]
        private BonusSettings _bonusSettings;
        [SerializeField]
        private EnemySettings _enemySettings;
        [SerializeField]
        private Transform[] _arealsOfEnemies;


        #region PrivateData

        private InputController _inputController;
        private PlayerController _playerController;
        private BonusesController _bonusController;
        private EnemiesController _enemiesController;
        private UiController _uiController;

        private List<IExecute> _executable;

        #endregion

        private void Awake(){
            _executable = new List<IExecute>();
            Extensions.Extentions.Init(_levelSettings);

            _playerModel.Init(maxHp: _unitSettings.PlayerHp);

             _inputController = new InputController(_inputModel);
             _playerController = new PlayerController(_inputModel, _playerModel, _unitSettings);
             _bonusController = new BonusesController(_bonusSettings);
             _enemiesController = new EnemiesController(_enemySettings, _arealsOfEnemies);
             _uiController = new UiController(_placeForUi, _uiSettings ,_inputModel, _playerModel);

            _executable.Add(_inputController);
            _executable.Add(_playerController);
            _executable.Add(_bonusController);
            _executable.Add(_enemiesController);

            _playerController.Init();
            _bonusController.Init();
            _enemiesController.Init();
            _uiController.Init();
            
            _inputModel.OnChangeGameState.Invoke(GameState.Game);
            
            _inputModel.OnChangeGameState += OnChangeGameState;
        }

        private void OnChangeGameState(GameState obj){
            // switch (obj){
            //     case GameState.None:
            //         break;
            //     case GameState.StartWindow:
            //         _inputController?.Dispose();
            //         _playerController?.Dispose();
            //         _bonusController?.Dispose();
            //         _enemiesController?.Dispose();
            //         break;
            //     case GameState.Game:
            //         _inputController.Init();
            //         _playerController.Init();
            //         _bonusController.Init();
            //         _enemiesController.Init();
            //         break;
            //     case GameState.WinWindow:
            //         break;
            //     case GameState.FailWindow:
            //         break;
            //     default:
            //         throw new ArgumentOutOfRangeException(nameof(obj), obj, null);
            // }
        }

        private void Update(){
            foreach (var execute in _executable){
                execute?.Execute(Time.deltaTime);
            }
        }

        ~Root(){
            _inputModel.OnChangeGameState -= OnChangeGameState;
        }
    }
}