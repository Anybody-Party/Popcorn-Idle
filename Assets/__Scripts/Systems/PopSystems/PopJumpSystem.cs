using Leopotam.Ecs;
using UnityEngine;

namespace Client
{
    public class PopJumpSystem : IEcsRunSystem
    {
        private GameData _gameData;
        private GameUI _gameUi;
        private EcsWorld _world;

        private EcsFilter<Pop, ReadyToJump>.Exclude<Timer<TimerPrepareToJump>> _filter;

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
                    Direction = (point.position - entityGo.Value.transform.position).normalized * Random.Range(_gameData.BalanceData.JumpPopcornForce.x, _gameData.BalanceData.JumpPopcornForce.y),
                    ForceMode = ForceMode.Impulse
                };
                entity.Get<LookingAt>().Target = point.position;
                entity.Get<InJump>();
                entity.Get<ChangePopEmotionRequest>().Emotion = PopEmotions.Happy;
                entity.Del<ReadyToJump>();
            }
        }
    }
}