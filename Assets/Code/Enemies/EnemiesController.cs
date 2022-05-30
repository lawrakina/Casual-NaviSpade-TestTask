using System.Collections.Generic;
using Code.Data;
using Code.Input;
using UnityEngine;


namespace Code.Enemies{
    internal class EnemiesController : IExecute{
        private readonly EnemySettings _enemySettings;
        private readonly IList<Transform> _arealsOfEnemies;
        private List<EnemyController> _enemies;

        private float _localTimer = 0;

        public EnemiesController(EnemySettings enemySettings, IList<Transform> arealsOfEnemies){
            _enemySettings = enemySettings;
            _arealsOfEnemies = arealsOfEnemies;

            _enemies = new List<EnemyController>();
        }

        public void Init(){
            for (int i = 0; i < _enemySettings.CountEnemiesAfterStart; i++){
                CreateNewEnemy();
            }
        }

        public void Execute(float deltaTime){
            foreach (var controller in _enemies){
                controller?.Execute(deltaTime);
            }

            _localTimer += deltaTime;
            if (_localTimer > _enemySettings.DeltaTimeBeforeInstantiateBonus){
                _localTimer = 0;
                CreateNewEnemy();
            }
        }

        private void CreateNewEnemy(){
            var enemyController = new EnemyController(
                _arealsOfEnemies[Random.Range(0, _arealsOfEnemies.Count)].transform.position,
                _enemySettings.EnemyPrefab, _enemySettings.SpeedMoving, _enemySettings.TimeToNewGoal);
            // enemyController.OnCollisionOnPlayer += OnCollisionOnPlayer;
            _enemies.Add(enemyController);
        }

        // private void OnCollisionOnPlayer(EnemyController obj){
            // obj.OnCollisionOnPlayer -= OnCollisionOnPlayer;
            // _enemies.Remove(obj);
        // }
    }
}