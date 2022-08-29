using Leopotam.Ecs;
using UnityEngine;

namespace Client
{
    public class PopSpawnSystem : IEcsRunSystem
    {
        private EcsWorld _world;
        private GameData _gameData;

        private EcsFilter<ConveyorLink, LaunchPop>.Exclude<Timer<TimerIntervalSpawnPop>> _filter;

        public void Run()
        {
            foreach (var idx in _filter)
            {
                ref EcsEntity entity = ref _filter.GetEntity(idx);
                ref ConveyorLink conveyor = ref entity.Get<ConveyorLink>();
                ref GameObjectLink conveyorGo = ref entity.Get<GameObjectLink>();

                entity.Get<Timer<TimerIntervalSpawnPop>>().Value = _gameData.RuntimeData.GetPopSpawnTime();

                EcsEntity popEntity = _world.NewEntity();
                popEntity.Get<Pop>() = new Pop
                {
                    Conveyor = conveyor,
                    ConveyorId = conveyor.Id,
                    ProductLineId = conveyor.ProductLineId
                };

                popEntity.Get<ReadyToLaunch>();

                Transform spawnPoint = conveyor.SpawnPoints[Random.Range(0, conveyor.SpawnPoints.Count)];
                popEntity.Get<SpawnPrefab>() = new SpawnPrefab
                {
                    Prefab = _gameData.StaticData.PopcornPrefab,
                    Position = spawnPoint.position,
                    Rotation = spawnPoint.rotation,
                    Parent = conveyorGo.Value.transform,
                    Entity = popEntity
                };

                popEntity.Get<PoolObjectRequest>();

                _world.NewEntity().Get<AddPopEvent>();
            }
        }
    }
}