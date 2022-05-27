using Code.Player;
using UnityEngine;


namespace Code.Data{
    [CreateAssetMenu(fileName = nameof(UserSettings), menuName = "Settings/" + nameof(UserSettings))]
    internal class UserSettings : ScriptableObject{
        [SerializeField]
        public PlayerView PlayerView;
    }
}