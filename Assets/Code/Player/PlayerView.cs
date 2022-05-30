using System;
using UnityEngine;


namespace Code.Player{
    [RequireComponent(typeof(CharacterController))] [RequireComponent(typeof(Animator))]
    public class PlayerView : MonoBehaviour, IPlayer{
        private Action _onGetUpBonus;
        private Action _onCollisionWithEnemy;
        
        private Rigidbody _rigidbody;
        private CharacterController _characterController;
        
        public AnimatorParameters AnimatorParameters{ get; set; }
        public Transform Transform => transform;
        public CharacterController CharacterController => _characterController;

        private void Awake(){
            _characterController = GetComponent<CharacterController>();
            AnimatorParameters = new AnimatorParameters(GetComponent<Animator>());
        }

        public void Init(Action onGetUpBonus, Action onCollisionWithEnemy){
            _onGetUpBonus = onGetUpBonus;
            _onCollisionWithEnemy = onCollisionWithEnemy;
        }

        public void GetUpBonus(){
            _onGetUpBonus?.Invoke();
        }

        public void Damage(){
            _onCollisionWithEnemy?.Invoke();
        }
    }
}