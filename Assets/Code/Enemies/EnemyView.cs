using System;
using UnityEngine;
using UnityEngine.AI;


namespace Code.Enemies{
    [RequireComponent(typeof(NavMeshAgent))]
    public sealed class EnemyView : MonoBehaviour{
        private Action<GameObject> _collisionOnObject;
        private NavMeshAgent Agent{ get; set; }

        private void Awake(){
            Agent = GetComponent<NavMeshAgent>();
        }

        public void Init(float speedMoving, Action<GameObject> collisionOnObject){
            Agent.speed = speedMoving;
            _collisionOnObject = collisionOnObject;
        }

        public void MoveTo(Vector3 position){
            Agent.SetDestination(position);
        }

        private void OnCollisionEnter(Collision other){
            Debug.Log($"Enemy collision: {other.gameObject}");
            _collisionOnObject?.Invoke(other.gameObject);
        }

        private void OnTriggerEnter(Collider other){
            _collisionOnObject?.Invoke(other.gameObject);
        }

        public void DestroySelf(){
            Destroy(gameObject);
        }
    }
}