using System.Collections.Generic;
using Code.Base;
using Code.Data;
using Code.Input;
using Code.Ui;
using UnityEngine;


namespace Code.Bonuses{
    public sealed class BonusesController : BaseController, IExecute{
        private readonly BonusSettings _bonusSettings;
        private List<BonusController> _bonuses;

        private float _localTimer = 0;

        public BonusesController(BonusSettings bonusSettings){
            _bonusSettings = bonusSettings;
            _bonuses = new List<BonusController>();
            Off();
        }

        public void Init(){
            On();
            for (int i = 0; i < _bonusSettings.CountBonusesAfterStart; i++){
                CreateNewBonus();
            }
        }

        public void Execute(float deltaTime){
            if(!IsOn) return;
            
            foreach (var controller in _bonuses){
                controller.Execute(deltaTime);
            }
            _localTimer += deltaTime;
            if (_localTimer > _bonusSettings.DeltaTimeBeforeInstantiateBonus){
                _localTimer = 0;
                CreateNewBonus();
            }
        }

        private void CreateNewBonus(){
            var bonusController = new BonusController(_bonusSettings.BonusPrefab);
            bonusController.OnCollisionOnPlayer += OnDestroyBonus;
            bonusController.OnDestroy += OnDestroyBonus;
            _bonuses.Add(bonusController);
        }


        private void OnDestroyBonus(BonusController controller){
            controller.OnCollisionOnPlayer -= OnDestroyBonus;
            controller.OnDestroy -= OnDestroyBonus;
            _bonuses.Remove(controller);
        }

        public void Clean(){
            Off();
            for (int i = 0; i < _bonuses.Count; i++){
                _bonuses[i].Destroy();
            }
            
            _bonuses.Clear();
        }
    }

    public class BaseController{
        private bool _isOn;
        public bool IsOn => _isOn;

        public void On(){
            _isOn = true;
        }
        public void Off(){
            _isOn = false;
        }
    }
}