using Leopotam.Ecs;

namespace Client
{
    public class PopPrepareToSellSystem : IEcsRunSystem
    {
        private GameData _gameData;
        private GameUI _gameUi;
        private EcsWorld _world;

        private EcsFilter<Pop, ReadyToSell>.Exclude<DelayTimer> _filter;

        public void Run()
        {
            foreach (var idx in _filter)
            {
                ref EcsEntity entity = ref _filter.GetEntity(idx);
                ref RigidbodyLink entityRb = ref entity.Get<RigidbodyLink>();

                entityRb.Value.isKinematic = true;
            }
        }
    }
}