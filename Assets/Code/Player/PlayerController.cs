using Code.Data;
using Code.Extensions;
using Code.Input;
using UnityEngine;


namespace Code.Player{
    public sealed class PlayerController : IExecute, IFixedExecute{
        private readonly InputModel _inputModel;
        private readonly PlayerModel _playerModel;
        private readonly UnitSettings _unitSettings;
        private PlayerView _player;
        private Vector3 _targetToMove;
        private bool _move = false;
        private float _leftTime;
        private bool _resistant;

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

        ~PlayerController(){
            _inputModel.OnMove -= OnMove;
            _playerModel.OnDied -= OnDied;
        }

        private void OnDied(){
            _player.AnimatorParameters.Die = true;
        }

        private void OnGetUpBonus(){
            _playerModel.HealthPoints++;
        }

        private void OnCollisionWithEnemy(){
            if (!_resistant){
                _resistant = true;
                _leftTime = _unitSettings.PlayerTimeResistance;
                _playerModel.HealthPoints--;
            }
        }

        private void OnMove(Vector3 newPosition){
            _targetToMove = newPosition;
            _move = true;
        }

        public void FixedExecute(float deltaTime){
            if (_move)
                if (Vector3.Distance(_targetToMove, _player.Transform.position) > 0.5f){
                    _player.transform.LookAt(
                        new Vector3(_targetToMove.x, _player.Transform.position.y, _targetToMove.z));
                    _player.CharacterController.Move(
                        (new Vector3(_targetToMove.x, 0, _targetToMove.z) -
                         _player.transform.position).normalized * (deltaTime * _unitSettings.PlayerSpeed));
                } else{
                    _move = false;
                }
        }

        public void Execute(float deltaTime){
            if (_leftTime > 0){//resistant in process
                _leftTime -= deltaTime;
            } else if (_resistant){//resistant is ended
                _resistant = false;
            }

            if (_player.CharacterController.velocity.sqrMagnitude > Vector3.kEpsilon){
                _player.AnimatorParameters.Run = true;
            }
        }
    }
}