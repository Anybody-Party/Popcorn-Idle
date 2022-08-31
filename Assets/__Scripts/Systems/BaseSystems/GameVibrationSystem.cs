using Leopotam.Ecs;
using UnityEngine;

namespace Client
{
    public class GameVibrationSystem : IEcsInitSystem, IEcsRunSystem
    {
        private GameData _gameData;

        private EcsFilter<SetVibrationStateEvent> _settingFilter;
        private EcsFilter<VibrationRequest> _eventFilter;

        public void Init()
        {
            Handheld.Vibrate();
        }

        public void Run()
        {
            foreach (var idx in _settingFilter)
            {
                EcsEntity entity = _settingFilter.GetEntity(idx);
                _gameData.StaticData.IsVibrationOn = !_gameData.StaticData.IsVibrationOn;
                entity.Del<SetVibrationStateEvent>();
            }

            foreach (var idx in _eventFilter)
            {
                EcsEntity entity = _eventFilter.GetEntity(idx);
                MoreMountains.NiceVibrations.MMVibrationManager.Haptic(entity.Get<VibrationRequest>().HapticType);
                entity.Del<VibrationRequest>();
            }
        }
    }
}