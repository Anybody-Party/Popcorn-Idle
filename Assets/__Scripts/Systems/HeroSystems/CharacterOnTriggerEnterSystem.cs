using Leopotam.Ecs;
using UnityEngine;

namespace Client
{
    public class CharacterOnTriggerEnterSystem : IEcsRunSystem
    {
        private GameData _gameData;

        private EcsFilter<Character, OnTriggerEnterEvent> _filter;

        public void Run()
        {
            foreach (var idx in _filter)
            {
                ref EcsEntity entity = ref _filter.GetEntity(idx);
                ref GameObjectLink entityGo = ref entity.Get<GameObjectLink>();
                ref ParticleSystemLink entityPs = ref entity.Get<ParticleSystemLink>();
                ref OnTriggerEnterEvent entityCollision = ref entity.Get<OnTriggerEnterEvent>();
                //Despawn
                if (entityCollision.Collider.gameObject.CompareTag(_gameData.StaticData.DespawnTag))
                {
                    //heroPs.ParticleSystems[0].ParticleSystem.Play();

                    entity.Del<Moving>();
                    entity.Del<LookingAt>();
                    entity.Get<DestroyTag>();
                }
            }
        }
    }
}
