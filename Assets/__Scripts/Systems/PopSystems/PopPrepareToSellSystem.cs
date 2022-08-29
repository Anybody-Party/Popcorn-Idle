using Leopotam.Ecs;

namespace Client
{
    public class PopPrepareToSellSystem : IEcsRunSystem
    {
        private GameData _gameData;
        private GameUI _gameUi;
        private EcsWorld _world;

        private EcsFilter<Pop, ReadyToSell>.Exclude<Timer<TimerToSellState>> _filter;
        private EcsFilter<HandLink> _handFilter;

        public void Run()
        {
            foreach (var idx in _filter)
            {
                ref EcsEntity entity = ref _filter.GetEntity(idx);
                ref GameObjectLink entityGo = ref entity.Get<GameObjectLink>();
                ref RigidbodyLink entityRb = ref entity.Get<RigidbodyLink>();
                ref Pop pop = ref entity.Get<Pop>();

                entityRb.Value.isKinematic = true;

                foreach (var hand in _handFilter)
                    if (pop.ProductLineId == _handFilter.Get1(hand).ProductLineId)
                        entityGo.Value.transform.SetParent(_handFilter.GetEntity(hand).Get<GameObjectLink>().Value.transform);
            }
        }
    }
}