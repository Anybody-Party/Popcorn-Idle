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
            }
        }
    }

    public class PopPrepareToSellSystem : IEcsRunSystem
    {
        private GameData _gameData;
        private GameUI _gameUi;
        private EcsWorld _world;

        private EcsFilter<Pop, ReadyToSell> _filter;

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
            }
        }
    }

    public class PopSellSystem : IEcsRunSystem
    {
        private GameData _gameData;
        private GameUI _gameUi;
        private EcsWorld _world;

        private EcsFilter<Pop, ReadyToSell> _filter;

        public void Run()
        {
            foreach (var idx in _filter)
            {
                ref EcsEntity entity = ref _filter.GetEntity(idx);
                ref Pop pop = ref entity.Get<Pop>();
                ref GameObjectLink entityGo = ref entity.Get<GameObjectLink>();
                ref RigidbodyLink entityRb = ref entity.Get<RigidbodyLink>();

                entityRb.Value.freezeRotation = false;

                Transform point = pop.Conveyor.JumpPoints[Random.Range(0, pop.Conveyor.JumpPoints.Count)];
                entity.Get<AddingForce>() = new AddingForce
                {
                    Direction = (point.position - entityGo.Value.transform.position).normalized * _gameData.StaticData.JumpPopcornForce,
                    ForceMode = ForceMode.Impulse
                };
                entity.Get<LookingAt>().Target = point.position;
                entity.Del<ReadyToJump>();
            }
        }
    }
}