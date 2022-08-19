﻿using Leopotam.Ecs;

namespace Client
{
    public class HeatingViewSystem : IEcsInitSystem, IEcsRunSystem
    {
        private GameData _gameData;
        private GameUI _gameUi;
        private EcsWorld _world;

        private EcsFilter<HeatingViewUpdateTimer>.Exclude<DelayTimer> _filter;
        private EcsFilter<ConveyorLink> _conveyorFilter;

        public void Init()
        {
            EcsEntity entity = _world.NewEntity();
            entity.Get<HeatingViewUpdateTimer>();
        }

        public void Run()
        {
            foreach (var idx in _filter)
            {
                float temp = _gameData.RuntimeData.Temperature / _gameData.StaticData.TemperatureCap.y;
                _gameUi.GameScreen.UpdateTemperatureProgressBar(temp);
                _gameUi.GameScreen.UpdateTemperatureText(_gameData.RuntimeData.Temperature);

                foreach (var conveyor in _conveyorFilter)
                    _conveyorFilter.Get1(conveyor).StoveMaterial.UpdateEmmision(temp, temp);

                _filter.GetEntity(idx).Get<DelayTimer>().Value = 0.02f;
            }
        }
    }
}