using Leopotam.Ecs;

namespace Client
{
    public class GameVibrationSystem : IEcsRunSystem
    {
        private GameData _gameData;

        private EcsFilter<SetVibrationStateEvent> _filter;

        public void Run()
        {
            foreach (var idx in _filter)
            {
                EcsEntity entity = _filter.GetEntity(idx);
                _gameData.StaticData.IsVibrationOn = !_gameData.StaticData.IsVibrationOn;
                entity.Del<SetVibrationStateEvent>();
            }
        }
    }
}