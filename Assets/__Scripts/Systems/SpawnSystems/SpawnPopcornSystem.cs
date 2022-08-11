using Leopotam.Ecs;
using System.Collections.Generic;
using UnityEngine;

namespace Client
{
    public class SpawnPopcornSystem : IEcsRunSystem
    {
        private EcsWorld _world;
        private GameData _gameData;

        private EcsFilter<Conveyor> _filter;

        public void Run()
        {
            if (_filter.IsEmpty())
                return;

            EcsEntity entity = _world.NewEntity();
            entity.Get<SpawnPrefab>() = new SpawnPrefab
            {
                Prefab = _gameData.StaticData.PopcornPrefab,
                Position = _gameData.SceneData.Conveyors[0].SpawnPoints[Random.Range(0, _gameData.SceneData.Conveyors[0].SpawnPoints.Count)].position,
                Rotation = Quaternion.identity,
                Parent = null,
                Entity = entity
            };
        }
    }
}