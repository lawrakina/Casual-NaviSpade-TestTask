using System;
using UnityEngine;
using UnityEngine.AI;


namespace Code.Player{
    [RequireComponent(typeof(NavMeshAgent))] [RequireComponent(typeof(Animator))]
    internal class PlayerView : MonoBehaviour, IMovable{
        public NavMeshAgent Agent{ get; set; }
        public AnimatorParameters AnimatorParameters{ get; set; }

        private void Awake(){
            Agent = GetComponent<NavMeshAgent>();
            AnimatorParameters = new AnimatorParameters(GetComponent<Animator>());
        }

        public void MoveTo(Vector3 position){
            Agent.SetDestination(position);
        }
    }

    internal class AnimatorParameters{
        private readonly Animator _animator;
        private static readonly int IsRun = Animator.StringToHash("IsRun");

        public AnimatorParameters(Animator animator){
            _animator = animator;
        }

        public bool Run{
            set{ _animator.SetBool(IsRun, value); }
        }
    }
}