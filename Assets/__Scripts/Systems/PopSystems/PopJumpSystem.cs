using Leopotam.Ecs;
using UnityEngine;

namespace Client
{
    public class PopJumpSystem : IEcsRunSystem
    {
        private GameData _gameData;
        private GameUI _gameUi;
        private EcsWorld _world;

        private EcsFilter<Pop, ReadyToJump>.Exclude<DelayTimer> _filter;

        public void Run()
        {
            foreach (var idx in _filter)
            {
                ref EcsEntity entity = ref _filter.GetEntity(idx);
                ref Pop pop = ref entity.Get<Pop>();
                ref GameObjectLink entityGo = ref entity.Get<GameObjectLink>();

                Transform point = pop.Conveyor.JumpPoints[Random.Range(0, pop.Conveyor.JumpPoints.Count)];
                entity.Get<AddingForce>() = new AddingForce
                {
                    Direction = (point.position - entityGo.Value.transform.position).normalized * _gameData.StaticData.JumpPopcornForce,
                    ForceMode = ForceMode.Impulse
                };
                entity.Get<LookingAt>().Target = point.position;
                entity.Get<InJump>();
                entity.Get<SetAnimationEvent>();
                entity.Del<ReadyToJump>();
            }
        }
    }

    public class HeatingSystem : IEcsRunSystem
    {
        private GameData _gameData;
        private GameUI _gameUi;
        private EcsWorld _world;

        private EcsFilter<ChangePressHeatingButtonEvent> _filter;

        public void Run()
        {
            float currentTemperature = _gameData.RuntimeData.Temperature;
            
            foreach (var idx in _filter)
            {
                ref EcsEntity entity = ref _filter.GetEntity(idx);
                ref ChangePressHeatingButtonEvent pressEvent = ref entity.Get<ChangePressHeatingButtonEvent>();

                if (pressEvent.IsPress)
                {
                    currentTemperature += _gameData.StaticData.HeatingSpeed * Time.deltaTime;
                    currentTemperature = Mathf.Clamp(currentTemperature, _gameData.StaticData.TemperatureMinCap, _gameData.StaticData.TemperatureMaxCap);
                    _gameData.RuntimeData.Temperature = currentTemperature;
                    return;
                }
            }

            currentTemperature -= _gameData.StaticData.ColdingSpeed * Time.deltaTime;
            currentTemperature = Mathf.Clamp(currentTemperature, _gameData.StaticData.TemperatureMinCap, _gameData.StaticData.TemperatureMaxCap);
            _gameData.RuntimeData.Temperature = currentTemperature;
        }
    }

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
                float temp = _gameData.RuntimeData.Temperature / _gameData.StaticData.TemperatureMaxCap;
                _gameUi.GameScreen.UpdateTemperatureProgressBar(temp);
                _gameUi.GameScreen.UpdateTemperatureText(_gameData.RuntimeData.Temperature);

                foreach (var conveyor in _conveyorFilter)
                    _conveyorFilter.Get1(conveyor).StoveMaterial.UpdateEmmision(temp, temp);

                _filter.GetEntity(idx).Get<DelayTimer>().Value = 0.5f;
            }
        }
    }
}