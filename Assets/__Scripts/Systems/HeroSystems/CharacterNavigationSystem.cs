using Leopotam.Ecs;
using UnityEngine;

namespace Client
{
    public class CharacterNavigationSystem : IEcsRunSystem
    {
        private GameData _gameData;
        private EcsWorld _world;

        private EcsFilter<Character> _filter;

        public void Run()
        {
            foreach (var hero in _filter)
            {
                ref EcsEntity heroEntity = ref _filter.GetEntity(hero);
                ref GameObjectLink heroEntityGo = ref heroEntity.Get<GameObjectLink>();

                Vector3 nextPosition = new Vector3();
                Vector3 heroPosition = heroEntityGo.Value.transform.position;
                Vector3 targetPosition = new Vector3();

                //nextPosition = 

                heroEntity.Get<SetAnimationEvent>();
                heroEntity.Get<Moving>().Target = nextPosition;
                heroEntity.Get<LookingAt>().Value = nextPosition;
            }
        }
    }
}