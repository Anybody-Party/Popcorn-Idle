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

        public void Run()
        {
            foreach (var idx in _filter)
            {
                ref EcsEntity entity = ref _filter.GetEntity(idx);
                entity.Del<UpgradeEvent>();
            }

            foreach (var idx in _showRequestFilter)
            {
                ref EcsEntity entity = ref _showRequestFilter.GetEntity(idx);
                _gameUi.SetShowStateUpgradeScreen(true);
                entity.Del<ShowUpgradeScreenRequest>();
            }
        }
    }
}