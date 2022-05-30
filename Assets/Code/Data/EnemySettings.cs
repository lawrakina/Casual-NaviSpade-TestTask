using Code.Enemies;
using UnityEngine;


namespace Code.Data{
    [CreateAssetMenu(fileName = nameof(EnemySettings), menuName = "Settings/" + nameof(EnemySettings))]
    public class EnemySettings: ScriptableObject{
        [SerializeField]
        public int CountEnemiesAfterStart = 1;
        [SerializeField]
        public int MaxCountEnemies = 5;
        [SerializeField]
        public EnemyView EnemyPrefab;
        [SerializeField]
        public float SpeedMoving = 2f;
        [SerializeField]
        public float DeltaTimeBeforeInstantiateBonus = 3f;
        [SerializeField]
        public float TimeToNewGoal = 5f;
    }
}