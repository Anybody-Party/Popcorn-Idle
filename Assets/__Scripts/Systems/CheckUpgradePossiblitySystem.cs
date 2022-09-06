﻿using Leopotam.Ecs;

namespace Client
{
    public class CheckUpgradePossiblitySystem : IEcsInitSystem, IEcsRunSystem
    {
        private GameData _gameData;
        private GameUI _gameUi;
        private EcsWorld _world;

        private EcsFilter<SpendGoldPopEvent> _spendGoldFilter;
        private EcsFilter<AddGoldPopEvent> _addGoldFilter;
        private EcsFilter<SpendMoneyEvent> _spendMoneyFilter;
        private EcsFilter<EarnMoneyEvent> _earnMoneyFilter;

        public void Init()
        {
            CheckCanUpgrade();
        }

        public void Run()
        {
            if (_spendGoldFilter.IsEmpty() && _addGoldFilter.IsEmpty() && _spendMoneyFilter.IsEmpty() && _earnMoneyFilter.IsEmpty())
                return;

            CheckCanUpgrade();
        }

        private void CheckCanUpgrade()
        {
            bool canBuyUpgrade = false;

            for (int i = 0; i < GameData.Instance.BalanceData.CommonUpgradeData.Count; i++)
                if (GameData.Instance.BalanceData.CommonUpgradeData[i].CanBuyIt())
                    canBuyUpgrade = true;

            for (int i = 0; i < GameData.Instance.BalanceData.EpicUpgradeData.Count; i++)
                if (GameData.Instance.BalanceData.EpicUpgradeData[i].CanBuyIt())
                    canBuyUpgrade = true;

            _gameUi.GameScreen.SetCanBuyUpgradeIndicator(canBuyUpgrade);

            if (!_gameData.PlayerData.TutrorialStates[(int)StaticData.Tutorials.Upgrade] && canBuyUpgrade)
                _world.NewEntity().Get<StartTutorialRequest>().Tutorial = StaticData.Tutorials.Upgrade;
        }
    }
}