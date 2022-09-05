using Leopotam.Ecs;
using UnityEngine;

namespace Client
{
    public class PopPopingSystem : IEcsRunSystem
    {
        private GameData _gameData;
        private GameUI _gameUi;
        private EcsWorld _world;

        private EcsFilter<Pop, Poping> _filter;

        public void Run()
        {
            foreach (var idx in _filter)
            {
                ref EcsEntity entity = ref _filter.GetEntity(idx);
                ref Pop pop = ref entity.Get<Pop>();
                ref GameObjectLink entityGo = ref entity.Get<GameObjectLink>();
                ref RigidbodyLink entityRb = ref entity.Get<RigidbodyLink>();

                entityGo.Value.transform.rotation = new Quaternion(0.0f, 180.0f, 0.0f, 0.0f);
                entityRb.Value.freezeRotation = true;

                Vector3 randomDirection = Random.onUnitSphere * 0.6f;
                entity.Get<AddingForce>() = new AddingForce
                {
                    Direction = new Vector3(randomDirection.x, 1.0f * Random.Range(_gameData.BalanceData.PopingPopcornForce.x, _gameData.BalanceData.PopingPopcornForce.y) , randomDirection.z),
                    ForceMode = ForceMode.Impulse
                };

                entity.Del<Poping>();
                entity.Get<Landing>();
                entity.Get<Timer<TimerBeforeGoToJump>>().Value = 0.5f;
                entity.Get<GoToJump>();
                entity.Get<ChangePopAnimationRequest>().Animation = StaticData.PopAnimations.IsPop;
                entity.Get<ChangePopEmotionRequest>().Emotion = StaticData.PopEmotions.Smile;
                entity.Get<RandomizePopRotationViewRequest>();

                _world.NewEntity().Get<VibrationRequest>().HapticType = MoreMountains.NiceVibrations.HapticTypes.LightImpact;
                _world.NewEntity().Get<PlaySoundRequest>().SoundName = StaticData.AudioSound.ExplosionSound;
                _world.NewEntity().Get<PlaySoundRequest>().SoundName = StaticData.AudioSound.JumpSound;
            }
        }
    }
}