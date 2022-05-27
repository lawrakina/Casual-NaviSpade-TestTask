using System.Collections.Generic;
using Code.Bonuses;
using Code.Data;
using Code.Enemies;
using Code.Input;
using Code.Player;
using UnityEngine;


namespace Code{
    public sealed class Root : MonoBehaviour{
        [SerializeField]
        private InputModel _inputModel;
        [SerializeField]
        private PlayerModel _playerModel;
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
        private List<IExecute> _executable;

        private void Awake(){
            _executable = new List<IExecute>();
            Extensions.Extentions.Init(_levelSettings);

            _playerModel.Init(maxHp: _unitSettings.PlayerHp);

            var inputController = new InputController(_inputModel);
            var playerController = new PlayerController(_inputModel, _playerModel, _unitSettings);
            var bonusController = new BonusesController(_bonusSettings);
            var enemiesController = new EnemiesController(_enemySettings, _arealsOfEnemies);

            _executable.Add(inputController);
            _executable.Add(playerController);
            _executable.Add(bonusController);
            _executable.Add(enemiesController);

            playerController.Init();
            bonusController.Init();
            enemiesController.Init();
        }

        private void Update(){
            foreach (var execute in _executable){
                execute?.Execute(Time.deltaTime);
            }
        }
    }
}