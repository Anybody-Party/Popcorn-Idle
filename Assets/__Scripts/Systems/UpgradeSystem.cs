using Leopotam.Ecs;

namespace Client
{
    public class UpgradeSystem : IEcsRunSystem
    {
        private GameData _gameData;
        private GameUI _gameUi;
        private EcsWorld _world;

        private EcsFilter<UpgradeEvent> _filter;
        private EcsFilter<ShowUpgradeScreenRequest> _showRequestFilter;

        private EcsFilter<SpendGoldPopEvent> _spendGoldFilter;
        private EcsFilter<AddGoldPopEvent> _addGoldFilter;
        private EcsFilter<SpendMoneyEvent> _spendMoneyFilter;
        private EcsFilter<EarnMoneyEvent> _earnMoneyFilter;

        public void Run()
        {
            foreach (var idx in _filter)
            {
                ref EcsEntity entity = ref _filter.GetEntity(idx);
                ref UpgradeEvent upgrade = ref entity.Get<UpgradeEvent>();

                foreach (var item in GameData.Instance.PlayerData.CommonUpgradeLevels)
                    if (item.UpgradeKey == upgrade.Key)
                        item.Level = upgrade.Level;

                foreach (var item in GameData.Instance.PlayerData.EpicUpgradeLevels)
                    if (item.UpgradeKey == upgrade.Key)
                        item.Level = upgrade.Level;

                GameData.Instance.PlayerData.UpdateUpgradeDataLevel();

                entity.Del<UpgradeEvent>();
            }

            foreach (var idx in _showRequestFilter)
            {
                ref EcsEntity entity = ref _showRequestFilter.GetEntity(idx);
                _gameUi.SetShowStateUpgradeScreen(true);
                entity.Del<ShowUpgradeScreenRequest>();
            }

            foreach (var idx in _spendGoldFilter)
                CheckCanUpgrade();
            foreach (var idx in _addGoldFilter)
                CheckCanUpgrade();
            foreach (var idx in _spendMoneyFilter)
                CheckCanUpgrade();
            foreach (var idx in _earnMoneyFilter)
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
        }
    }
}