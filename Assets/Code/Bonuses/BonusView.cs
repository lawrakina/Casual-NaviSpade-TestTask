using System;
using UnityEngine;


namespace Code.Bonuses{
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(CapsuleCollider))]
    public class BonusView : MonoBehaviour{
        private Action<Collision> _collisionOnObject;
        private void OnCollisionEnter(Collision other){
            _collisionOnObject?.Invoke(other);
        }

        public void Init(Action<Collision> collisionOnObject){
            _collisionOnObject = collisionOnObject;
        }

        public void DestroySelf(){
            Destroy(gameObject);
        }
    }
}