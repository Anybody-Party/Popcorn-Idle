using Leopotam.Ecs;

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
                    DeleteAllMoving(ref entity);

                    entity.Get<DespawnTag>();
                    entity.Get<ChangePopViewRequest>().PopBodyView = PopBodyView.RawCorn;
                    entity.Get<ChangePopEmotionRequest>().Emotion = PopEmotions.Empty;
                    entity.Get<RigidbodyLink>().Value.isKinematic = false;
                }

                if (entityCollision.Collider.gameObject.CompareTag(_gameData.StaticData.CookingZoneTag))
                    entity.Get<Cooking>();

                if (entityCollision.Collider.gameObject.CompareTag(_gameData.StaticData.GroundTag))
                {
                    DeleteAllMoving(ref entity);
                    entity.Get<CleanIt>();
                    entity.Get<ChangePopViewRequest>().PopBodyView = PopBodyView.PopcornWithoutLimbs;
                    entity.Get<ChangePopEmotionRequest>().Emotion = PopEmotions.Scary;
                }

                if (!entity.Has<ReadyToSell>() && entityCollision.Collider.gameObject.CompareTag(_gameData.StaticData.SellZoneTag))
                {
                    entity.Get<GetMoneyForPopInSellZone>().PopEntity = entity;

                    DeleteAllMoving(ref entity);

                    entity.Get<Timer<TimerToSellState>>().Value = 3.0f;
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

        private void DeleteAllMoving(ref EcsEntity pop)
        {
            pop.Del<VelocityMoving>();
            pop.Del<TransformMoving>();
            pop.Del<LookingAt>();
            pop.Del<GoToJump>();
        }
    }
}

