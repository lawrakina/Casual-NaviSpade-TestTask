using System;
using Code.Input;
using UnityEngine;


namespace Code.Data{
    [CreateAssetMenu(fileName = nameof(GameProcessModel), menuName = "Models/" + nameof(GameProcessModel))]
    public class GameProcessModel : ScriptableObject{
        public Action<GameState> OnChangeGameState = delegate(GameState state){  };
    }
}