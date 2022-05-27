using Code.Data;
using UnityEngine;


namespace Code.Extensions{
    public static class Extentions{
        private static LevelSettings _settings;

        public static void Init(LevelSettings settings){
            _settings = settings;
        }

        public static Vector3 GetEmptyPoint(){
            var count = 0;
            while (true){
                count++;
                var center = new Vector3(Random.Range(-_settings.Width / 2, _settings.Width / 2), 1f,
                    Random.Range(-_settings.Length / 2, _settings.Length / 2));
                var maxColliders = 1;
                var hitColliders = new Collider[maxColliders];
                var numColliders = Physics.OverlapSphereNonAlloc(center, 1, hitColliders);
                if (numColliders == 0){
                    return center;
                }

                if (count > 100) return Vector3.zero;
            }
        }

        public static T SpawnObject<T>(Vector3 spawnPoint, T prefab) where T : MonoBehaviour{
            var objects = Object.Instantiate<T>(prefab);
            objects.transform.Translate(spawnPoint);
            return objects.GetComponent<T>();
        }
    }
}