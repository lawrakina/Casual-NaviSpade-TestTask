using Code.Base;
using Code.Data;
using UnityEngine;


namespace Code.Input{
    public sealed class InputController : IExecute{
        private InputModel _inputModel;

        public InputController(InputModel inputModel){
            _inputModel = inputModel;
        }

        public void Execute(float deltaTime){
            if (UnityEngine.Input.GetMouseButtonDown(0) || UnityEngine.Input.touchCount == 1){
                Vector3 mouse = UnityEngine.Input.mousePosition;
                Ray castPoint = Camera.main.ScreenPointToRay(mouse);
                RaycastHit hit;
                if (Physics.Raycast(castPoint, out hit, Mathf.Infinity)){
                    _inputModel.OnMove?.Invoke(hit.point);
                }
            }
        }
    }
}