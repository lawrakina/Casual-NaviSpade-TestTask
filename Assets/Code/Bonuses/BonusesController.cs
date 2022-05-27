using System.Collections.Generic;
using Code.Data;
using Code.Input;


namespace Code.Bonuses{
    public sealed class BonusesController : IExecute{
        private readonly BonusSettings _bonusSettings;
        private List<BonusController> _bonuses;

        private float _localTimer = 0;

        public BonusesController(BonusSettings bonusSettings){
            _bonusSettings = bonusSettings;
            _bonuses = new List<BonusController>();
        }

        public void Init(){
            for (int i = 0; i < _bonusSettings.CountBonusesAfterStart; i++){
                CreateNewBonus();
            }
        }

        public void Execute(float deltaTime){
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
            bonusController.OnCollisionOnPlayer += OnCollisionOnPlayer;
            _bonuses.Add(bonusController);
        }

        private void OnCollisionOnPlayer(BonusController controller){
            controller.OnCollisionOnPlayer -= OnCollisionOnPlayer;
            _bonuses.Remove(controller);
        }
    }
}