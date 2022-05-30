using System;
using UnityEngine;


namespace Code.Bonuses{
    [RequireComponent(typeof(Rigidbody))] 
    [RequireComponent(typeof(CapsuleCollider))]
    public class BonusView : MonoBehaviour, IBonus{
        private Action<Collision> _collisionOnObject;
        private Action _onDestroy;

        private void OnCollisionEnter(Collision other){
            _collisionOnObject?.Invoke(other);
        }

        public void Init(Action<Collision> collisionOnObject, Action destroy){
            _collisionOnObject = collisionOnObject;
            _onDestroy = destroy;
        }

        public void DestroySelf(){
            Destroy(gameObject);
        }

        public void Destroy(){
            _onDestroy?.Invoke();
        }
    }
}