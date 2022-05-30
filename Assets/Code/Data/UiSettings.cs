using Code.Ui;
using Code.Ui.Fail;
using Code.Ui.Game;
using UnityEngine;


namespace Code.Data{
    [CreateAssetMenu(fileName = nameof(UiSettings), menuName = "Settings/" + nameof(UiSettings))]
    internal class UiSettings : ScriptableObject{
        [SerializeField]
        public StartView StartView;
        [SerializeField]
        public GameView GameView;
        [SerializeField]
        public WinView WinView;
        [SerializeField]
        public FailView FailView;
    }
}