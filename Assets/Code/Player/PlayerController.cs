using Code.Data;
using Code.Extensions;
using Code.Input;
using Code.Units;
using UnityEngine;


namespace Code.Player{
    public sealed class PlayerController : IExecute{
        private readonly InputModel _inputModel;
        private readonly PlayerModel _playerModel;
        private readonly UnitSettings _unitSettings;
        private PlayerView _player;

        public PlayerController(InputModel inputModel, PlayerModel playerModel, UnitSettings unitSettings){
            _inputModel = inputModel;
            _playerModel = playerModel;
            _unitSettings = unitSettings;
        }

        public void Init(){
            _player = Extentions.SpawnObject(Extentions.GetEmptyPoint(), _unitSettings.PlayerView);
            _player.Init(OnGetUpBonus, OnCollisionWithEnemy);
            _inputModel.OnMove += OnMove;
            _playerModel.OnDied += OnDied;
        }

        private void OnDied(){
            _player.AnimatorParameters.Die = true;
        }

        private void OnGetUpBonus(){
            _playerModel.HealthPoints++;
        }

        private void OnCollisionWithEnemy(){
            _playerModel.HealthPoints--;
        }
        
        private void OnMove(Vector3 newPosition){
             _player.MoveTo(newPosition);
        }


        ~PlayerController(){
            _inputModel.OnMove -= OnMove;
            _playerModel.OnDied -= OnDied;
        }

        public void Execute(float deltaTime){
            if (_player.Agent.velocity.sqrMagnitude > Vector3.kEpsilon){
                _player.AnimatorParameters.Run = true;
            }   
        }
    }
}