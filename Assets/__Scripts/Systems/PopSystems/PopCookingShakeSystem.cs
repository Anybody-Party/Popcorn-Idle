using Leopotam.Ecs;
using UnityEngine;

namespace Client
{
    public class PopCookingShakeSystem : IEcsRunSystem
    {
        private GameData _gameData;
        private GameUI _gameUi;
        private EcsWorld _world;

        private EcsFilter<Pop, Cooking>.Exclude<Done, ShakeTimer> _filter;
        private EcsFilter<Pop, Cooking, ShakeTimer> _tiemrFilter;

        public void Run()
        {
            foreach (var idx in _filter)
            {
                ref EcsEntity entity = ref _filter.GetEntity(idx);
                ref GameObjectLink entityGo = ref entity.Get<GameObjectLink>();

                entity.Get<AddingForce>() = new AddingForce
                {
                    Direction = Vector3.up * Random.Range(_gameData.BalanceData.CookingShakePopcornForce.x, _gameData.BalanceData.CookingShakePopcornForce.y),
                    ForceMode = ForceMode.Impulse
                };

                entity.Get<ShakeTimer>().Value = 0.2f;
            }

            foreach (var idx in _tiemrFilter)
            {
                ref EcsEntity entity = ref _tiemrFilter.GetEntity(idx);
                ref ShakeTimer shakeTimer = ref entity.Get<ShakeTimer>();

                shakeTimer.Value -= Time.deltaTime;
                if (shakeTimer.Value <= 0.0f)
                    entity.Del<ShakeTimer>();
            }
        }
    }
}