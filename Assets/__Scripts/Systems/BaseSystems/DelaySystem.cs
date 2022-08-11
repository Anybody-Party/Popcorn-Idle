﻿using Leopotam.Ecs;
using UnityEngine;

namespace Client
{
    public partial class TimeSystem : IEcsRunSystem
    {
        private EcsFilter<DelayTimer> _delaysFilter;

        public void Run()
        {
            foreach (var delay in _delaysFilter)
            {
                ref EcsEntity delayTimerEntity = ref _delaysFilter.GetEntity(delay);
                ref DelayTimer delayTimer = ref _delaysFilter.Get1(delay);
                delayTimer.Value -= Time.deltaTime;
                if (delayTimer.Value < 0.01f)
                    delayTimerEntity.Del<DelayTimer>();
            }
        }
    }
}