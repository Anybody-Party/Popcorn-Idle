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
                entity.Del<ReadyToJump>();
                entity.Get<SetAnimationEvent>();
            }
        }
    }

    public class PopPrepareToSellSystem : IEcsRunSystem
    {
        private GameData _gameData;
        private GameUI _gameUi;
        private EcsWorld _world;

        private EcsFilter<Pop, ReadyToSellTag> _filter;

        public void Run()
        {
            foreach (var idx in _filter)
            {
                ref EcsEntity entity = ref _filter.GetEntity(idx);
                ref RigidbodyLink entityRb = ref entity.Get<RigidbodyLink>();

                entityRb.Value.isKinematic = true;
            }
        }
    }

    public class PopSellSystem : IEcsRunSystem
    {
        private GameData _gameData;
        private GameUI _gameUi;
        private EcsWorld _world;

        private EcsFilter<Pop, ReadyToSellTag> _filter;

        public void Run()
        {
            foreach (var idx in _filter)
            {
                ref EcsEntity entity = ref _filter.GetEntity(idx);
            }
        }
    }

    public class PopEmotionsHandlerSystem : IEcsRunSystem
    {
        private GameData _gameData;
        private GameUI _gameUi;
        private EcsWorld _world;

        private EcsFilter<Pop, PopCookingDoneEvent> _doneFilter;

        public void Run()
        {
            foreach (var idx in _doneFilter)
            {
                ref EcsEntity entity = ref _doneFilter.GetEntity(idx);
                ref PopcornViewLink popView = ref entity.Get<PopcornViewLink>();

                popView.DoneBody.SetActive(true);
                popView.RawBody.SetActive(false);
            }
        }
    }
}