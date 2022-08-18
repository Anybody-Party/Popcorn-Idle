using Leopotam.Ecs;
using UnityEngine;

namespace Client
{
    public class PopPopingSystem : IEcsRunSystem
    {
        private GameData _gameData;
        private GameUI _gameUi;
        private EcsWorld _world;

        private EcsFilter<Pop, Poping> _filter;

        public void Run()
        {
            foreach (var idx in _filter)
            {
                ref EcsEntity entity = ref _filter.GetEntity(idx);
                ref Pop pop = ref entity.Get<Pop>();
                ref GameObjectLink entityGo = ref entity.Get<GameObjectLink>();
                ref RigidbodyLink entityRb = ref entity.Get<RigidbodyLink>();

                entityGo.Value.transform.rotation = new Quaternion(0.0f, 180.0f, 0.0f, 0.0f);
                entityRb.Value.freezeRotation = true;

                Vector3 randomDirection = Random.onUnitSphere;
                entity.Get<AddingForce>() = new AddingForce
                {
                    Direction = new Vector3(randomDirection.x, 1.0f * _gameData.StaticData.PopingPopcornForce, randomDirection.z),
                    ForceMode = ForceMode.Impulse
                };

                entity.Del<Poping>();
                entity.Get<Landing>();
                entity.Get<DelayTimer>().Value = 0.5f;
                entity.Get<GoToJump>();
            }
        }
    }
}