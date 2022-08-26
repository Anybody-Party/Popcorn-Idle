using Leopotam.Ecs;

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
                float temperature = _gameData.RuntimeData.Temperature / _gameData.BalanceData.CurrentTemperatureCap.y;
                float temperatureWithMax = _gameData.RuntimeData.Temperature / _gameData.BalanceData.MaxTemperature;
                _gameUi.GameScreen.UpdateTemperatureProgressBar(temperature);
                _gameUi.GameScreen.UpdateHeatingButtonColor(temperatureWithMax);
                _gameUi.GameScreen.UpdateTemperatureText(_gameData.RuntimeData.Temperature);

                foreach (var conveyor in _conveyorFilter)
                {
                    _conveyorFilter.Get1(conveyor).StoveMaterial.UpdateEmmision(temperatureWithMax, temperatureWithMax);
                    if(temperature > 220.0f)
                    {
                        if (!_conveyorFilter.Get1(conveyor).HeatSteam.activeInHierarchy)
                            _conveyorFilter.Get1(conveyor).HeatSteam.SetActive(true);
                        if (!_conveyorFilter.Get1(conveyor).AirHeatDistortion.activeInHierarchy)
                            _conveyorFilter.Get1(conveyor).AirHeatDistortion.SetActive(true);
                    }
                    else
                    {
                        if (_conveyorFilter.Get1(conveyor).HeatSteam.activeInHierarchy)
                            _conveyorFilter.Get1(conveyor).HeatSteam.SetActive(false);
                        if (_conveyorFilter.Get1(conveyor).AirHeatDistortion.activeInHierarchy)
                            _conveyorFilter.Get1(conveyor).AirHeatDistortion.SetActive(false);
                    }
                }

                _filter.GetEntity(idx).Get<DelayTimer>().Value = 0.02f;
            }
        }
    }
}