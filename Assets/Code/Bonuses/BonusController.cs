using System;
using Code.Extensions;
using Code.Input;
using Code.Player;
using UnityEngine;


namespace Code.Bonuses{
    public class BonusController: IExecute{
        private BonusView _view;
        public Action<BonusController> OnCollisionOnPlayer;
        public Action<BonusController> OnDestroy;

        public BonusController(BonusView bonusPrefab){
            _view = Extentions.SpawnObject(Extentions.GetEmptyPoint(), bonusPrefab);
            _view.Init(CollisionOnObject, Destroy);
        }

        private void CollisionOnObject(Collision info){
            if (info.gameObject.TryGetComponent(out IPlayer player)){
                _view.DestroySelf();
                player.GetUpBonus();
                OnCollisionOnPlayer?.Invoke(this);
            }
        }

        private void Destroy(){
            OnDestroy?.Invoke(this);
        }

        public void Execute(float deltaTime){
            _view.transform.Rotate( Vector3.up , 50 * deltaTime);
        }
    }
}