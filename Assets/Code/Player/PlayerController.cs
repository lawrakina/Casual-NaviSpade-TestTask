using Code.Base;
using Code.Bonuses;
using Code.Data;
using Code.Extensions;
using Code.Input;
using UnityEngine;


namespace Code.Player{
    public sealed class PlayerController : BaseController, IExecute, IFixedExecute, ICleaned{
        private readonly GameProcessModel _gameModel;
        private readonly InputModel _inputModel;
        private readonly PlayerModel _playerModel;
        private readonly UnitSettings _unitSettings;
        private PlayerView _player;
        private Vector3 _targetToMove;
        private bool _move = false;
        private float _leftTime;
        private bool _resistant;

        public PlayerController(GameProcessModel gameModel, InputModel inputModel, PlayerModel playerModel,
            UnitSettings unitSettings){
            _gameModel = gameModel;
            _inputModel = inputModel;
            _playerModel = playerModel;
            _unitSettings = unitSettings;
            Off();
        }

        public void Init(){
            On();
            _player = Extentions.SpawnObject(Extentions.GetEmptyPoint(), _unitSettings.PlayerView);
            _player.Init(OnGetUpBonus, OnCollisionWithEnemy);
            _inputModel.OnMove += OnMove;
            _playerModel.OnDied += OnDied;
            _playerModel.HealthPoints = _unitSettings.PlayerHp;
        }

        ~PlayerController(){
            Off();
            _inputModel.OnMove -= OnMove;
            _playerModel.OnDied -= OnDied;
        }

        public void Clean(){
            Off();
            GameObject.Destroy(_player.gameObject);
        }

        private void OnDied(){
            Off();
            _gameModel.OnChangeGameState.Invoke(GameState.FailWindow);
        }

        private void OnGetUpBonus(){
            _playerModel.Bonuses++;
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
            if(!IsOn) return;
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
            if(!IsOn) return;
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