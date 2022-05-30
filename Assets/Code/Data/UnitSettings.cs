using Code.Player;
using UnityEngine;


namespace Code.Data{
    [CreateAssetMenu(fileName = nameof(UnitSettings), menuName = "Settings/" + nameof(UnitSettings))]
    public class UnitSettings : ScriptableObject{
        [SerializeField]
        public PlayerView PlayerView;
        [SerializeField]
        public int PlayerHp = 3;
        [SerializeField]
        public float PlayerSpeed = 5f;
        public float PlayerTimeResistance = 5f;
    }
}