using Code.Bonuses;
using UnityEngine;


namespace Code.Data{
    [CreateAssetMenu(fileName = nameof(BonusSettings), menuName = "Settings/" + nameof(BonusSettings))]
    public class BonusSettings: ScriptableObject{
        [SerializeField]
        public int CountBonusesAfterStart = 5;
        [SerializeField]
        public BonusView BonusPrefab;
        [SerializeField]
        public float DeltaTimeBeforeInstantiateBonus = 3f;
    }
}