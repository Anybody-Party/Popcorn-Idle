﻿using Leopotam.Ecs;
using UnityEngine;

namespace Client
{
    public class EarningViewSystem : IEcsRunSystem
    {
        private GameData _gameData;
        private GameUI _gameUi;
        private EcsWorld _world;

        private EcsFilter<CreateEarnViewRequest> _requestFilter;
        private EcsFilter<WorldTextLink, EarnView>.Exclude<Done> _initInfoFilter;
        private EcsFilter<WorldTextLink, EarnView, Done>.Exclude<DespawnTag, DelayTimer> _despawnInfoFilter;

        public void Run()
        {
            foreach (var idx in _requestFilter)
            {
                ref EcsEntity entity = ref _requestFilter.GetEntity(idx);
                ref EarnView entityEarnView = ref entity.Get<EarnView>();

                entity.Get<PoolObject>();

                entity.Get<SpawnPrefab>() = new SpawnPrefab
                {
                    Prefab = _gameData.StaticData.EarnInfoPrefab,
                    Position = entityEarnView.Position,
                    Rotation = Quaternion.identity,
                    Parent = null,
                    Entity = entity
                };

                entity.Get<PoolObjectRequest>();
                entity.Del<CreateEarnViewRequest>();
            }

            foreach (var idx in _initInfoFilter)
            {
                ref EcsEntity entity = ref _initInfoFilter.GetEntity(idx);
                ref EarnView entityEarnView = ref entity.Get<EarnView>();
                ref WorldTextLink entityEarnInfoView = ref entity.Get<WorldTextLink>();

                entityEarnInfoView.Value.text = $"+{entityEarnView.Value}";
                entity.Get<DelayTimer>().Value = 1.0f;
                entity.Get<Done>();
            }

            foreach (var idx in _despawnInfoFilter)
                _despawnInfoFilter.GetEntity(idx).Get<DespawnTag>();
        }
    }
}