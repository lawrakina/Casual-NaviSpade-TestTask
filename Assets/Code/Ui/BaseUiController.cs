using System;


namespace Code.Ui{
    public class BaseUiController{
        public event Action OnHide;
        public event Action OnShow;

        public void Hide(){
            OnHide?.Invoke();
        }

        public void Show(){
            OnShow?.Invoke();
        }
    }
}