using System;
using UnityEngine;


namespace Code.Ui{
    internal class BaseView: MonoBehaviour{

        public void Hide(){
            gameObject.SetActive(false);
        }

        public void Show(){
            gameObject.SetActive(true);
        }
    }
}