using Leopotam.Ecs;

namespace Client
{
    public class SettingSystem : IEcsRunSystem
    {
        private GameData _gameData;
        private GameUI _gameUi;
        private EcsWorld _world;

        private EcsFilter<ShowSettingScreenRequest> _filter;

        public void Run()
        {
            foreach (var idx in _filter)
            {
                ref EcsEntity entity = ref _filter.GetEntity(idx);
                _gameUi.SetShowStateSettingScreen(true);
                entity.Del<ShowSettingScreenRequest>();
            }
        }
    }
}