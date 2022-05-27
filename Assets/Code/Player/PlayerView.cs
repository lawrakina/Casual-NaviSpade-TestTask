using System;
using Code.Units;
using UnityEngine;
using UnityEngine.AI;


namespace Code.Player{
    [RequireComponent(typeof(NavMeshAgent))] [RequireComponent(typeof(Animator))]
    public class PlayerView : MonoBehaviour, IPlayer, IMovable{
        private Action _onGetUpBonus;
        private Action _onCollisionWithEnemy;
        public NavMeshAgent Agent{ get; set; }
        public AnimatorParameters AnimatorParameters{ get; set; }

        private void Awake(){
            Agent = GetComponent<NavMeshAgent>();
            AnimatorParameters = new AnimatorParameters(GetComponent<Animator>());
        }

        public void MoveTo(Vector3 position){
            Agent.SetDestination(position);
        }

        public void GetUpBonus(){
            _onGetUpBonus?.Invoke();
        }

        public void Init(Action onGetUpBonus, Action onCollisionWithEnemy){
            _onGetUpBonus = onGetUpBonus;
            _onCollisionWithEnemy = onCollisionWithEnemy;
        }
    }
}