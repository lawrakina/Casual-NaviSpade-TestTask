using System;
using Code.Units;
using UnityEngine;
using UnityEngine.AI;


namespace Code.Enemies{
    [RequireComponent(typeof(NavMeshAgent))]
    public sealed class EnemyView : MonoBehaviour, IMovable{
        private Action<Collision> _collisionOnObject;
        private NavMeshAgent Agent{ get; set; }

        private void Awake(){
            Agent = GetComponent<NavMeshAgent>();
        }

        public void Init(float speedMoving, Action<Collision> collisionOnObject){
            Agent.speed = speedMoving;
            _collisionOnObject = collisionOnObject;
        }

        public void MoveTo(Vector3 position){
            Agent.SetDestination(position);
        }

        private void OnCollisionEnter(Collision other){
            _collisionOnObject?.Invoke(other);
        }

        public void DestroySelf(){
            Destroy(gameObject);
        }
    }
}