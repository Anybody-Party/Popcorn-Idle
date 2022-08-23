using Leopotam.Ecs;

namespace Client
{
    public class MoneyCounterSystem : IEcsInitSystem, IEcsRunSystem
    {
        private GameData _gameData;
        private GameUI _gameUi;
        private EcsWorld _world;

        private EcsFilter<EarnMoneyEvent> _filter;

        public void Init()
        {
            _gameData.PlayerData.Money = _gameData.BalanceData.StartMoney;
            _gameUi.GameScreen.UpdateMoneyText(_gameData.PlayerData.Money);
        }

        public void Run()
        {
            foreach (var idx in _filter)
            {
                ref EcsEntity entity = ref _filter.GetEntity(idx);
                _gameData.PlayerData.Money += _filter.Get1(idx).Value;
                _gameUi.GameScreen.UpdateMoneyText(_gameData.PlayerData.Money);
                entity.Del<EarnMoneyEvent>();
            }
        }
    }

    public class MoneyInSecCounterSystem : IEcsInitSystem, IEcsRunSystem
    {
        private GameData _gameData;
        private GameUI _gameUi;
        private EcsWorld _world;

        private EcsFilter<MoneyInSecUpdateTimer>.Exclude<DelayTimer> _filter;

        public void Init()
        {
            _gameData.PlayerData.MoneyInSec = 0;

            EcsEntity entity = _world.NewEntity();
            entity.Get<MoneyInSecUpdateTimer>();
        }

        public void Run()
        {
            foreach (var idx in _filter)
            {
                //_gameData.PlayerData.HeatingMaxTemperatureUpgradeLevel;
                //_gameData.PlayerData.HeatingSpeedUpgradeLevel;
                //_gameData.PlayerData.HeatingAreaUpgradeLevel;

                //_gameData.PlayerData.LaunchSpeedUpgradeLevel;
                //_gameData.PlayerData.ConveyorSpeedUpgradeLevel;
                //_gameData.PlayerData.BagSizeUpgradeLevel;

                //_gameData.PlayerData.ConveyorLevels;
                //_gameData.PlayerData.ProductLineLevels;

                //_gameData.BalanceData.BaseSpawnPopTime;
                //_gameData.BalanceData.BaseCookingTime;
                //_gameData.BalanceData.BaseHeatingSpeed;
                //_gameData.BalanceData.BaseColdingSpeed;

                //float popcornSpeed = _gameData.RuntimeData.IsTapSpeedUpWorking ? _gameData.BalanceData.BasePopcornSpeed * _gameData.BalanceData.TapSpeedUpSpeedCoef : _gameData.BalanceData.BasePopcornSpeed;
                //float spawnSpeed = _gameData.BalanceData.BaseSpawnPopTime;
                //double popEarn = _gameData.BalanceData.BasePopSellReward * _gameData.BalanceData.PopSellRewardMultiplier * _gameData.PlayerData.EarnForPopUpgradeLevel;

                //double moneyInSecFromPops = (popEarn
                //            ) // base earn
                //            / (10.0f / popcornSpeed *

                //            ); // TODO: magic 10 - time for sell pop












                //double moneyInSecFromBags = (_gameData.BalanceData.BaseBagSellReward
                //    * _gameData.BalanceData.BagSellRewardMultiplier
                //    * _gameData.PlayerData.EarnForBagUpgradeLevel
                //    ) // base earn
                //    / (250.0f); // TODO: magic 250 - time for sell bag

                //_gameData.PlayerData.MoneyInSec = moneyInSecFromPops + moneyInSecFromBags;
                //_gameUi.GameScreen.UpdateMoneyInSecText(_gameData.PlayerData.MoneyInSec);

                //_filter.GetEntity(idx).Get<DelayTimer>().Value = 0.02f;
            }
        }
    }
}