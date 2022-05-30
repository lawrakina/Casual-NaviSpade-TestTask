using System.Collections.Generic;
using Code.Base;
using Code.Bonuses;
using Code.Data;
using Code.Input;
using UnityEngine;


namespace Code.Enemies{
    internal class EnemiesController : BaseController, IExecute{
        private readonly EnemySettings _enemySettings;
        private readonly IList<Transform> _arealsOfEnemies;
        private List<EnemyController> _enemies;

        private float _localTimer = 0;

        public EnemiesController(EnemySettings enemySettings, IList<Transform> arealsOfEnemies){
            _enemySettings = enemySettings;
            _arealsOfEnemies = arealsOfEnemies;

            _enemies = new List<EnemyController>();
            Off();
        }

        public void Init(){
            On();
            for (int i = 0; i < _enemySettings.CountEnemiesAfterStart; i++){
                CreateNewEnemy();
            }
        }

        public void Execute(float deltaTime){
            if (!IsOn) return;
            foreach (var controller in _enemies){
                controller?.Execute(deltaTime);
            }

            _localTimer += deltaTime;
            if (_localTimer > _enemySettings.DeltaTimeBeforeInstantiateBonus){
                _localTimer = 0;
                if(_enemies.Count < _enemySettings.MaxCountEnemies)
                    CreateNewEnemy();
            }
        }

        private void CreateNewEnemy(){
            var enemyController = new EnemyController(
                _arealsOfEnemies[Random.Range(0, _arealsOfEnemies.Count)].transform.position,
                _enemySettings.EnemyPrefab, _enemySettings.SpeedMoving, _enemySettings.TimeToNewGoal);
            _enemies.Add(enemyController);
            enemyController.OnCollisionOnPlayer += OnCollisionOnPlayer;
        }

        private void OnCollisionOnPlayer(EnemyController obj){
            obj.OnCollisionOnPlayer -= OnCollisionOnPlayer;
            obj.Destroy();
            _enemies.Remove(obj);
        }

        public void Clean(){
            Off();
            for (var index = 0; index < _enemies.Count; index++){
                _enemies[index].Destroy();
            }

            _enemies.Clear();
        }
    }
}