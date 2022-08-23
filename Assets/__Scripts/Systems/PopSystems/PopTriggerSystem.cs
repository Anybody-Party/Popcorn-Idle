using Leopotam.Ecs;
using UnityEngine;

namespace Client
{
    public class PopTriggerSystem : IEcsRunSystem
    {
        private EcsWorld _world;
        private GameData _gameData;

        private EcsFilter<Pop, OnTriggerEnterEvent> _enterFilter;
        private EcsFilter<Pop, OnTriggerExitEvent> _exitfilter;

        public void Run()
        {
            foreach (var idx in _enterFilter)
            {
                ref EcsEntity entity = ref _enterFilter.GetEntity(idx);
                ref GameObjectLink entityGo = ref entity.Get<GameObjectLink>();
                //ref ParticleSystemLink entityPs = ref entity.Get<ParticleSystemLink>();
                ref OnTriggerEnterEvent entityCollision = ref entity.Get<OnTriggerEnterEvent>();
                ref PopcornViewLink popView = ref entity.Get<PopcornViewLink>();

                //Despawn
                if (entity.Has<PoolObject>() && entityCollision.Collider.gameObject.CompareTag(_gameData.StaticData.DespawnTag))
                {
                    //heroPs.ParticleSystems[0].ParticleSystem.Play();

                    entity.Del<VelocityMoving>();
                    entity.Del<TransformMoving>();
                    entity.Del<LookingAt>();
                    entity.Del<GoToJump>();

                    entity.Get<DespawnTag>();
                    entity.Get<ChangePopViewRequest>().PopBodyView = PopBodyView.RawCorn;
                    entity.Get<ChangePopEmotionRequest>().Emotion = PopEmotions.Smile;
                }

                if (entityCollision.Collider.gameObject.CompareTag(_gameData.StaticData.CookingZoneTag))
                    entity.Get<Cooking>();

                if (!entity.Has<ReadyToSell>() && entityCollision.Collider.gameObject.CompareTag(_gameData.StaticData.SellZoneTag))
                {
                    entity.Get<GetMoneyForPopInSellZone>().PopEntity = entity;

                    entity.Del<VelocityMoving>();
                    entity.Del<TransformMoving>();
                    entity.Del<LookingAt>();
                    entity.Del<GoToJump>();

                    entity.Get<DelayTimer>().Value = 3.0f;
                    entity.Get<ChangePopViewRequest>().PopBodyView = PopBodyView.PopcornWithoutLimbs;
                    entity.Get<ChangePopEmotionRequest>().Emotion = PopEmotions.Empty;
                }
            }

            foreach (var idx in _exitfilter)
            {
                ref EcsEntity entity = ref _exitfilter.GetEntity(idx);
                ref GameObjectLink entityGo = ref entity.Get<GameObjectLink>();
                ref OnTriggerExitEvent entityCollision = ref entity.Get<OnTriggerExitEvent>();

                if (entity.Has<Cooking>() && entityCollision.Collider.gameObject.CompareTag(_gameData.StaticData.CookingZoneTag))
                    entity.Del<Cooking>();
            }
        }
    }
}
