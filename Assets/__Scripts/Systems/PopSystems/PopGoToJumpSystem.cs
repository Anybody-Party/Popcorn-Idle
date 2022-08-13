﻿using Leopotam.Ecs;
using UnityEngine;

namespace Client
{
    public class PopGoToJumpSystem : IEcsRunSystem
    {
        private GameData _gameData;
        private GameUI _gameUi;
        private EcsWorld _world;

        private EcsFilter<Pop, GoToJump>.Exclude<TransformMoving, DelayTimer> _filter;

        public void Run()
        {
            foreach (var idx in _filter)
            {
                ref EcsEntity entity = ref _filter.GetEntity(idx);
                ref Pop pop = ref entity.Get<Pop>();

                Transform point = pop.Conveyor.PrepareJumpPoints[Random.Range(0, pop.Conveyor.PrepareJumpPoints.Count)];
                entity.Get<TransformMoving>() = new TransformMoving
                {
                    Target = point.position,
                    Accuracy = 0.05f,
                    Speed = _gameData.StaticData.PopcornSpeed
                };
                entity.Get<LookingAt>().Target = point.position;
            }
        }
    }
}