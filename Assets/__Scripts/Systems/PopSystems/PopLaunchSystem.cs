using Leopotam.Ecs;
using UnityEngine;

namespace Client
{
    public class PopLaunchSystem : IEcsRunSystem
    {
        private GameData _gameData;
        private GameUI _gameUi;
        private EcsWorld _world;

        private EcsFilter<GameObjectLink, ReadyToLaunch> _filter;

        public void Run()
        {
            foreach (var idx in _filter)
            {
                ref EcsEntity entity = ref _filter.GetEntity(idx);
                ref GameObjectLink entityGo = ref entity.Get<GameObjectLink>();
                ref RigidbodyLink entityRb = ref entity.Get<RigidbodyLink>();
                ref PopcornViewLink entityView = ref entity.Get<PopcornViewLink>();

                entityView.RawBody.transform.Rotate(Vector3.up, Random.Range(0, 360.0f));

                Utility.ResetRigibodyVelocity(entityRb.Value);
                entity.Get<AddingForce>().Direction = entityGo.Value.transform.up * Random.Range(_gameData.BalanceData.LaunchPopcornForce.x, _gameData.BalanceData.LaunchPopcornForce.y);
                entity.Get<AddingForce>().ForceMode = ForceMode.Impulse;
                entity.Get<Landing>();

                entity.Del<ReadyToLaunch>();
            }
        }
    }
}