﻿using Leopotam.Ecs;
using UnityEngine;

namespace Client
{
    public class TransformMovingSystem : IEcsRunSystem
    {
        private EcsFilter<TransformMoving> _movingFilter;

        public void Run()
        {
            if (_movingFilter.IsEmpty())
                return;

            foreach (var movingObject in _movingFilter)
            {
                ref EcsEntity movingEntity = ref _movingFilter.GetEntity(movingObject);
                ref GameObjectLink movingEntityGo = ref movingEntity.Get<GameObjectLink>();

                ref TransformMoving moving = ref movingEntity.Get<TransformMoving>();

                moving.Speed = moving.Speed == 0 ? 2 : moving.Speed;
                moving.Accuracy = moving.Accuracy == 0 ? 0.1f : moving.Accuracy;

                movingEntityGo.Value.transform.position = Vector3.MoveTowards(movingEntityGo.Value.transform.position, moving.Target, moving.Speed);

                if (Vector3.Distance(movingEntityGo.Value.transform.position, moving.Target) < moving.Accuracy)
                {
                    movingEntity.Del<TransformMoving>();
                    //moving.CompleteAction?.Invoke();
                    movingEntity.Get<MovingCompleteEvent>();
                }
            }
        }
    }
}