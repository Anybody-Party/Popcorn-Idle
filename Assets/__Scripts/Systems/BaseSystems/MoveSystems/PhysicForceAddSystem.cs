using Leopotam.Ecs;

namespace Client
{
    public class PhysicForceAddSystem : IEcsRunSystem
    {
        private EcsFilter<AddingForce, RigidbodyLink> _filter;

        public void Run()
        {
            foreach (var idx in _filter)
            {
                ref EcsEntity entity = ref _filter.GetEntity(idx);
                ref GameObjectLink entityGo = ref entity.Get<GameObjectLink>();
                ref RigidbodyLink entityRb = ref entity.Get<RigidbodyLink>();
                ref AddingForce force = ref entity.Get<AddingForce>();

                entityRb.Value.AddForce(force.Direction, force.ForceMode);

                entity.Get<Landing>();
                entity.Del<AddingForce>();
            }
        }
    }
}
