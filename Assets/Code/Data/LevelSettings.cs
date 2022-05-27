using UnityEngine;


namespace Code.Data{
    [CreateAssetMenu(fileName = nameof(LevelSettings), menuName = "Settings/" + nameof(LevelSettings))]
    internal class LevelSettings : ScriptableObject{
        [SerializeField]
        public float Width = 20f;
        [SerializeField]
        public float Length = 20f;
    }
}