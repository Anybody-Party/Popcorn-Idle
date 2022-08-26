using Leopotam.Ecs;
using UnityEngine;

namespace Client
{
    public class MoneyInSecCounterSystem : IEcsInitSystem, IEcsRunSystem
    {
        private GameData _gameData;
        private GameUI _gameUi;
        private EcsWorld _world;

        private EcsFilter<MoneyInSecUpdateTimer>.Exclude<DelayTimer> _filter;

        public void Init()
        {
            _gameData.PlayerData.MoneyInSec = 0;

            _world.NewEntity().Get<MoneyInSecUpdateTimer>();
        }

        public void Run()
        {
            foreach (var idx in _filter)
            {
                float heatingSpeed = _gameData.BalanceData.BaseHeatingSpeed * _gameData.PlayerData.HeatingMaxTemperatureUpgrade.Level * _gameData.PlayerData.HeatingSpeedUpgradeLevel * _gameData.PlayerData.HeatingMinTemperatureUpgradeLevel;
                float popcornSpeed = _gameData.RuntimeData.IsTapSpeedUpWorking ? _gameData.BalanceData.BasePopcornSpeed * _gameData.BalanceData.TapSpeedUpSpeedCoef : _gameData.BalanceData.BasePopcornSpeed;
                popcornSpeed += _gameData.PlayerData.ConveyorSpeedUpgradeLevel * 0.5f;
                float spawnTime= _gameData.BalanceData.BaseSpawnPopTime * Mathf.Pow(0.9f, _gameData.PlayerData.LaunchSpeedUpgradeLevel);
                float cookingTime = (_gameData.BalanceData.BaseCookingTime) * Mathf.Pow(0.9f, heatingSpeed);

                double popEarn = (_gameData.BalanceData.BasePopSellReward * _gameData.BalanceData.PopSellRewardMultiplier) * Mathf.Pow(1.16f, _gameData.PlayerData.EarnForPopUpgradeLevel);
                double moneyInSecFromPops = popEarn / ((spawnTime + cookingTime) / (popcornSpeed + heatingSpeed));

                double bagEarn = (_gameData.BalanceData.BaseBagSellReward * _gameData.BalanceData.BagSellRewardMultiplier) * Mathf.Pow(1.07f, _gameData.PlayerData.EarnForBagUpgradeLevel);
                double moneyInSecFromBags = bagEarn / ((spawnTime + cookingTime) / (popcornSpeed + heatingSpeed)) * 20;

                //Debug.Log($"heatingSpeed {heatingSpeed}");
                //Debug.Log($"popcornSpeed {popcornSpeed}");
                //Debug.Log($"spawnTime {spawnTime}");
                //Debug.Log($"cookingTime {cookingTime}");
                //Debug.Log($"popEarn {popEarn}");
                //Debug.Log($"bagEarn {bagEarn}");
                //Debug.Log($"time: {(spawnTime + cookingTime)} * {(popcornSpeed + heatingSpeed)}");
                //Debug.Log($"moneyInSecFromPops: {moneyInSecFromPops}");
                //Debug.Log($"moneyInSecFromBags: {moneyInSecFromBags}");

                int conveyorCounter = 0;
                foreach (var item in _gameData.PlayerData.ConveyorBuyed)
                    if (item)
                        conveyorCounter++;

                _gameData.PlayerData.MoneyInSec = moneyInSecFromPops + moneyInSecFromBags;
                _gameData.PlayerData.MoneyInSec *= conveyorCounter;
                _gameData.PlayerData.MoneyInSec *= 0.1f;

                _gameUi.GameScreen.UpdateMoneyInSecText(_gameData.PlayerData.MoneyInSec);

                _filter.GetEntity(idx).Get<DelayTimer>().Value = 1.0f;
            }
        }
    }
}