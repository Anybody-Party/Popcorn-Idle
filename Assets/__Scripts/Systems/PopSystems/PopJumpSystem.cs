using DG.Tweening;
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

                Vector3 point = Vector3.zero;
                float distance = 100.0f;
                for (int i = 0; i < pop.Conveyor.JumpPoints.Count; i++)
                    if (Vector3.Distance(entityGo.Value.transform.position, pop.Conveyor.JumpPoints[i].position) < distance)
                        point = pop.Conveyor.JumpPoints[i].position;

                entity.Get<AddingForce>() = new AddingForce
                {
                    Direction = (point - entityGo.Value.transform.position).normalized * Random.Range(_gameData.BalanceData.JumpPopcornForce.x, _gameData.BalanceData.JumpPopcornForce.y),
                    ForceMode = ForceMode.Impulse
                };
                entity.Get<LookingAt>().Target = point;
                entity.Get<InJump>();
                entity.Get<ChangePopEmotionRequest>().Emotion = PopEmotions.Happy;
                entity.Del<ReadyToJump>();
            }
        }
    }

    public class GoldPopTapSystem : IEcsRunSystem
    {
        private GameData _gameData;
        private GameUI _gameUi;
        private EcsWorld _world;

        private EcsFilter<RaycastEvent> _tapFilter;
        private EcsFilter<GoldPop>.Exclude<IsThisGoldTakenMarker, Timer<TimerGoldTaken>> _filter;
        private EcsFilter<GoldPop, TimerDoneEvent<TimerGoldTaken>> _timerDoneFilter;

        public void Run()
        {
            foreach (var tap in _tapFilter)
            {
                ref EcsEntity tapEntity = ref _tapFilter.GetEntity(tap);
                ref RaycastEvent raycast = ref tapEntity.Get<RaycastEvent>();

                if (raycast.GameObject.CompareTag(_gameData.StaticData.GoldPopcornTag))
                {
                    foreach (var idx in _filter)
                    {
                        ref EcsEntity entity = ref _filter.GetEntity(idx);
                        ref Pop pop = ref entity.Get<Pop>();
                        ref PopcornViewLink popView = ref entity.Get<PopcornViewLink>();
                        ref GameObjectLink popGo = ref entity.Get<GameObjectLink>();

                        popView.SpeedUpTrail.SetActive(true);

                        popGo.Value.transform.DOMove(_gameUi.GameScreen.GetGoldPopPosition(), _gameData.BalanceData.BaseGetGoldPopTime).SetEase(Ease.InCirc);
                        entity.Get<IsThisGoldTakenMarker>();
                        entity.Get<Timer<TimerGoldTaken>>().Value = _gameData.BalanceData.BaseGetGoldPopTime;
                        entity.Get<ChangeAnimationRequest>().Animation = PopAnimations.IsGoldTaken;
                    }
                }
            }

            foreach (var idx in _timerDoneFilter)
            {
                ref EcsEntity entity = ref _timerDoneFilter.GetEntity(idx);
                ref TransformMoving moving = ref entity.Get<TransformMoving>();
                ref PopcornViewLink popView = ref entity.Get<PopcornViewLink>();

                popView.SpeedUpTrail.SetActive(false);

                PopExtensions.StopAllMoving(ref entity);
                PopExtensions.PrepareToDespawn(ref entity);
                entity.Del<TimerDoneEvent<TimerGoldTaken>>();
                entity.Del<IsThisGoldTakenMarker>();

                _world.NewEntity().Get<AddGoldPopEvent>();
            }
        }
    }
}