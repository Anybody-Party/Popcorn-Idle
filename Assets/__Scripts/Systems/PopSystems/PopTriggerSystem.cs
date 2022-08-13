using Leopotam.Ecs;
using UnityEngine;

namespace Client
{
    public class PopTriggerSystem : IEcsRunSystem
    {
        private EcsWorld _world;
        private GameData _gameData;

        private EcsFilter<Pop, OnTriggerEnterEvent> _enterFilter;
        //private EcsFilter<Pop, OnTriggerExitEvent> _filter;

        public void Run()
        {
            foreach (var idx in _enterFilter)
            {
                ref EcsEntity entity = ref _enterFilter.GetEntity(idx);
                ref GameObjectLink entityGo = ref entity.Get<GameObjectLink>();
                //ref ParticleSystemLink entityPs = ref entity.Get<ParticleSystemLink>();
                ref OnTriggerEnterEvent entityCollision = ref entity.Get<OnTriggerEnterEvent>();

                //Despawn
                if (entityCollision.Collider.gameObject.CompareTag(_gameData.StaticData.DespawnTag))
                {
                    //heroPs.ParticleSystems[0].ParticleSystem.Play();

                    entity.Del<VelocityMoving>();
                    entity.Del<TransformMoving>();
                    entity.Del<LookingAt>();
                    entity.Get<DestroyTag>();
                }
                
                if (entityCollision.Collider.gameObject.CompareTag(_gameData.StaticData.CookingZoneTag))
                    entity.Get<Cooking>();

                if (entityCollision.Collider.gameObject.CompareTag(_gameData.StaticData.SellZoneTag))
                {
                    entity.Get<ReadyToSell>();

                    EcsEntity eventEntity = _world.NewEntity();
                    eventEntity.Get<GetMoneyForPopInSellZone>().PopEntity = entity;
                }
            }

            //foreach (var idx in _enterFilter)
            //{
            //    ref EcsEntity entity = ref _enterFilter.GetEntity(idx);
            //    ref GameObjectLink entityGo = ref entity.Get<GameObjectLink>();
            //    ref OnTriggerExitEvent entityCollision = ref entity.Get<OnTriggerExitEvent>();

            //    if (entityCollision.Collider.gameObject.CompareTag(_gameData.StaticData.CookingZoneTag))
            //        entity.Del<Cooking>();
            //}
        }
    }
}
