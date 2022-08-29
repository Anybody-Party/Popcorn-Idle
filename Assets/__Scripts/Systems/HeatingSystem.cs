using Leopotam.Ecs;
using UnityEngine;

namespace Client
{
    public class HeatingSystem : IEcsRunSystem
    {
        private GameData _gameData;
        private GameUI _gameUi;
        private EcsWorld _world;

        private EcsFilter<PressHeatingButtonEvent> _filter;
        private EcsFilter<ReleaseHeatingButtonEvent> _releaseFilter;

        public void Run()
        {
            float currentTemperature = _gameData.RuntimeData.Temperature;

            foreach (var idx in _filter)
            {
                ref EcsEntity entity = ref _filter.GetEntity(idx);

                currentTemperature += _gameData.RuntimeData.GetHeatingSpeed() * Time.deltaTime;
                currentTemperature = Mathf.Clamp(currentTemperature, _gameData.RuntimeData.GetMinTemperature(), _gameData.RuntimeData.GetMaxTemperature());
                _gameData.RuntimeData.Temperature = currentTemperature;
                foreach (var item in _releaseFilter)
                {
                    entity.Del<PressHeatingButtonEvent>();
                    _releaseFilter.GetEntity(item).Del<ReleaseHeatingButtonEvent>();
                }
                return;
            }

            currentTemperature -= _gameData.RuntimeData.GetColdingSpeed() * Time.deltaTime;
            currentTemperature = Mathf.Clamp(currentTemperature, _gameData.RuntimeData.GetMinTemperature(), _gameData.RuntimeData.GetMaxTemperature());
            _gameData.RuntimeData.Temperature = currentTemperature;
        }
    }
}