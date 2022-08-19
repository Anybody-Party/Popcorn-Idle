using Leopotam.Ecs;

namespace Client
{
    public class ConveyorSystem : IEcsInitSystem, IEcsRunSystem
    {
        private GameData _gameData;
        private GameUI _gameUi;
        private EcsWorld _world;

        private EcsFilter<ConveyorLink> _filter;
        private EcsFilter<BuyNewConveyorEvent> _buyFilter;

        public void Init()
        {
            foreach (var idx in _filter)
            {
                ref EcsEntity entity = ref _filter.GetEntity(idx);

                ref ConveyorLink conveyor = ref entity.Get<ConveyorLink>();
                ref GameObjectLink entityGo = ref entity.Get<GameObjectLink>();

                for (int i = 0; i < _gameData.SceneData.Conveyors.Count; i++)
                    if (conveyor.Id == i)
                        _filter.Get1(idx).Level = _gameData.PlayerData.ConveyorLevels[i];

                if(conveyor.Level > 0)
                {
                    entity.Get<LaunchPopTimerTag>();
                    entityGo.Value.SetActive(true);
                }
                else
                {
                    entityGo.Value.SetActive(false);
                }
            }
        }

        public void Run()
        {
            foreach (var buy in _buyFilter)
            {
                ref EcsEntity buyEventEntity = ref _filter.GetEntity(buy);
                ref BuyNewConveyorEvent buyEvent = ref buyEventEntity.Get<BuyNewConveyorEvent>();

                foreach (var idx in _filter)
                {
                    ref EcsEntity entity = ref _filter.GetEntity(idx);

                    ref ConveyorLink conveyor = ref entity.Get<ConveyorLink>();
                    ref GameObjectLink entityGo = ref entity.Get<GameObjectLink>();

                    for (int i = 0; i < _gameData.SceneData.Conveyors.Count; i++)
                        if (conveyor.Id == buyEvent.ConveyorNum)
                        {
                            _gameData.PlayerData.ConveyorLevels[conveyor.Id] = 1;
                            conveyor.Level = 1;
                            entity.Get<LaunchPopTimerTag>();
                        }
                }

                buyEventEntity.Del<BuyNewConveyorEvent>();
            }
        }
    }
}