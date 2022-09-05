﻿using Leopotam.Ecs;

namespace Client
{
    public class PopStopAllMovingSystem : IEcsRunSystem
    {
        private GameData _gameData;
        private GameUI _gameUi;
        private EcsWorld _world;

        private EcsFilter<Pop, StopAllMovingRequest> _filter;

        public void Run()
        {
            foreach (var idx in _filter)
            {
                ref EcsEntity entity = ref _filter.GetEntity(idx);
                entity.Del<VelocityMoving>();
                entity.Del<TransformMoving>();
                entity.Del<LookingAt>();
                entity.Del<GoToJump>();
                entity.Del<StopAllMovingRequest>();
            }
        }
    }
}