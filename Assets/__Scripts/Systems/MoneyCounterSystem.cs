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
}