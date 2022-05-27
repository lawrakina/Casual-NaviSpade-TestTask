using System;
using UnityEngine;


namespace Code.Input{
    [CreateAssetMenu(fileName = nameof(InputModel), menuName = "Models/" + nameof(InputModel))]
    public class InputModel: ScriptableObject{
        public Action<Vector3> OnMove = _ => {};
    }
}