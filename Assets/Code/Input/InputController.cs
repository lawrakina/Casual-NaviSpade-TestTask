using UnityEngine;


namespace Code.Input{
    public class InputController : MonoBehaviour{
        [SerializeField]
        private InputModel _inputModel;

        private void Update(){
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