using Leopotam.Ecs;

namespace Client
{
    public class PopEarningSystem : IEcsRunSystem
    {
        private GameData _gameData;
        private GameUI _gameUi;
        private EcsWorld _world;

        private EcsFilter<GetMoneyForPopInSellZone>.Exclude<ReadyToSell> _popFilter;

        public void Run()
        {
            foreach (var idx in _popFilter)
            {
                ref EcsEntity entity = ref _popFilter.GetEntity(idx);
                ref GameObjectLink entityGo = ref entity.Get<GameObjectLink>();

                double reward = _gameData.BalanceData.BasePopSellReward * _gameData.BalanceData.PopSellRewardMultiplier; // TODO: Add additions modificator

                _world.NewEntity().Get<EarnMoneyEvent>().Value = reward;

                EcsEntity earnViewEntity = _world.NewEntity();
                earnViewEntity.Get<EarnView>() = new EarnView
                {
                    Value = reward,
                    Position = entityGo.Value.transform.position
                };
                earnViewEntity.Get<CreateEarnViewRequest>();

                entity.Get<GetMoneyForPopInSellZone>().PopEntity.Get<ReadyToSell>();
                entity.Del<GetMoneyForPopInSellZone>();
            }
        }
    }
}