using Leopotam.Ecs;

namespace Client
{
    public class TurnOffAtMovingCompleteSystem : IEcsRunSystem
    {
        private EcsFilter<TurnOffAtMovingComplete, MovingCompleteEvent> _filter;

        public void Run()
        {
            foreach (var idx in _filter)
            {
                ref EcsEntity entity = ref _filter.GetEntity(idx);
                ref GameObjectLink entityGo = ref entity.Get<GameObjectLink>();
                entityGo.Value.SetActive(false);
            }
        }
    }
}