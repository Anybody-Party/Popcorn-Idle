using Leopotam.Ecs;

namespace Client
{
    public class PopCounterSystem : IEcsRunSystem
    {
        private EcsWorld _world;
        private GameData _gameData;
        private GameUI _gameUi;

        private EcsFilter<AddPopEvent> _filter;
        private EcsFilter<AddGoldPopEvent> _goldFilter;

        public void Run()
        {
            foreach (var idx in _filter)
            {
                _gameData.PlayerData.PopcornAmount += 1;
                _gameUi.GameScreen.UpdatePopcornAmountText(_gameData.PlayerData.PopcornAmount);
                _filter.GetEntity(idx).Del<AddPopEvent>();
            }

            foreach (var idx in _goldFilter)
            {
                _gameData.PlayerData.GoldPopcornAmount += 1;
                _gameUi.GameScreen.UpdateGoldPopcornAmountText(_gameData.PlayerData.GoldPopcornAmount);
                _filter.GetEntity(idx).Del<AddGoldPopEvent>();
            }
        }
    }
}