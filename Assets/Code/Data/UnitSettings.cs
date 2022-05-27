using Code.Player;
using Code.Units;
using UnityEngine;


namespace Code.Data{
    [CreateAssetMenu(fileName = nameof(UnitSettings), menuName = "Settings/" + nameof(UnitSettings))]
    public class UnitSettings : ScriptableObject{
        [SerializeField]
        public PlayerView PlayerView;
        [SerializeField]
        public int PlayerHp = 5;
    }
}