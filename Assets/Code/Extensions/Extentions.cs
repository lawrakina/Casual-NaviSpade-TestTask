using UnityEngine;


namespace Code.Extensions{
    internal static class Extentions{
        public static Vector3 GetEmptyPoint(float width, float length, float radius){
            var count = 0;
            while (true){
                count++;
                var center = new Vector3(Random.Range(-width / 2, width / 2), 1f,
                    Random.Range(-length / 2, length / 2));
                var maxColliders = 1;
                var hitColliders = new Collider[maxColliders];
                var numColliders = Physics.OverlapSphereNonAlloc(center, radius, hitColliders);
                if (numColliders == 0){
                    return center;
                }

                if (count > 100) return Vector3.zero;
            }
        }
    }
}