﻿using Leopotam.Ecs;
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

                Utility.ResetRigibodyVelocity(entityRb.Value);
                entity.Get<AddingForce>().Direction = entityGo.Value.transform.up * Random.Range(_gameData.StaticData.LaunchPopcornForce.x, _gameData.StaticData.LaunchPopcornForce.y);
                entity.Get<AddingForce>().ForceMode = ForceMode.Impulse;
                entity.Get<Landing>();

                entity.Del<ReadyToLaunch>();
            }
        }
    }
}