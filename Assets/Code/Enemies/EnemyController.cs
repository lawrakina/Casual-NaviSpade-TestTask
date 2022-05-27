using System;
using Code.Bonuses;
using Code.Extensions;
using Code.Input;
using Code.Player;
using UnityEngine;


namespace Code.Enemies{
    internal class EnemyController : IExecute{
        private readonly float _timeToNewGoal;
        private EnemyView _view;
        public Action<EnemyController> OnCollisionOnPlayer;
        private float _localTimer = 0;

        public EnemyController(Vector3 spawnPoint, EnemyView prefab, float speedMoving,
            float timeToNewGoal){
            _timeToNewGoal = timeToNewGoal;
            _view = Extentions.SpawnObject(spawnPoint, prefab);
            _view.Init(speedMoving, CollisionOnObject);

            SetDestination();
        }

        private void SetDestination(){
            _view.MoveTo(Extentions.GetEmptyPoint());
        }

        private void CollisionOnObject(Collision info){
            if (info.gameObject.TryGetComponent(out IBonus bonus)){
                bonus.Destroy();
            }

            if (info.gameObject.TryGetComponent(out IPlayer player)){
                _view.DestroySelf();
                player.Damage();
                OnCollisionOnPlayer?.Invoke(this);
            }
        }

        public void Execute(float deltaTime){
            _localTimer += deltaTime;
            if (_localTimer > _timeToNewGoal){
                _localTimer = 0;
                SetDestination();
            }
        }
    }
}