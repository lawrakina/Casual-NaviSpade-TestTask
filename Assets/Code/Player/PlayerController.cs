using Code.Data;
using Code.Extensions;
using Code.Input;
using UnityEngine;


namespace Code.Player{
    public class PlayerController : MonoBehaviour{
        [SerializeField]
        private InputModel _inputModel;
        [SerializeField]
        private UserSettings _userSettings;
        [SerializeField]
        private LevelSettings _levelSettings;
        private PlayerView _player;

        private void Awake(){
            _player = SpawnPlayer(Extentions.GetEmptyPoint(_levelSettings.Width, _levelSettings.Length, radius: 1.0f),
                _userSettings.PlayerView);
            
            _inputModel.OnMove += OnMove;
        }

        private void Update(){
            if (_player.Agent.velocity.sqrMagnitude > Vector3.kEpsilon){
                _player.AnimatorParameters.Run = true;
            }
        }

        private void OnMove(Vector3 newPosition){
             _player.MoveTo(newPosition);
        }

        private PlayerView SpawnPlayer(Vector3 spawnPoint, PlayerView prefab){
            var player = Instantiate(prefab);
            player.transform.Translate(spawnPoint);
            return player.GetComponent<PlayerView>();
        }

        ~PlayerController(){
            _inputModel.OnMove -= OnMove;
        }
    }
}