using Leopotam.Ecs;

namespace Client
{
    public class ConveyorInitSystem : IEcsInitSystem
    {
        private GameData _gameData;
        private GameUI _gameUi;
        private EcsWorld _world;

        private EcsFilter<ConveyorLink> _filter;

        public void Init()
        {
            foreach (var idx in _filter)
            {
                ref EcsEntity entity = ref _filter.GetEntity(idx);

                ref ConveyorLink conveyor = ref entity.Get<ConveyorLink>();
                entity.Get<LaunchPopTimerTag>();

                for (int i = 0; i < _gameData.PlayerData.ConveyorLevels.Count; i++)
                    if (conveyor.Id == i)
                        _filter.Get1(idx).Level = _gameData.PlayerData.ConveyorLevels[i];
            }
        }
    }
}