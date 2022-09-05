using DG.Tweening;
using Leopotam.Ecs;
using UnityEngine;

namespace Client
{
    public class PopCleanSystem : IEcsRunSystem
    {
        private GameData _gameData;
        private GameUI _gameUi;
        private EcsWorld _world;

        private EcsFilter<Pop, CleanIt>.Exclude<Timer<TimerForPopClean>> _filter;
        private EcsFilter<Pop, CleanIt, TimerDoneEvent<TimerForPopClean>> _timerDoneFilter;

        public void Run()
        {
            foreach (var idx in _filter)
            {
                ref EcsEntity entity = ref _filter.GetEntity(idx);
                ref Pop pop = ref entity.Get<Pop>();
                ref GameObjectLink entityGo = ref entity.Get<GameObjectLink>();

                entity.Get<ChangePopViewRequest>().PopBody = StaticData.PopBody.PopcornWithoutLimbs;
                entity.Get<ChangePopEmotionRequest>().Emotion = StaticData.PopEmotions.Scary;
                entity.Get<StopAllMovingRequest>();

                entity.Get<Timer<TimerForPopClean>>().Value = _gameData.BalanceData.CleanTime;
                Transform tr = entityGo.Value.transform;
                Transform parentBefore = tr.parent;
                tr.SetParent(null);
                Vector3 scaleBefore = tr.localScale;
                tr.DOScale(Vector3.zero, _gameData.BalanceData.CleanTime - 0.01f).SetEase(Ease.InOutBounce).OnComplete(() => { tr.DOScale(scaleBefore, 0.0f); tr.SetParent(parentBefore); });
            }

            foreach (var idx in _timerDoneFilter)
            {
                ref EcsEntity entity = ref _timerDoneFilter.GetEntity(idx);
                ref GameObjectLink entityGo = ref entity.Get<GameObjectLink>();

                entity.Get<PrepareToDespawnRequest>();
                entity.Del<Timer<TimerForPopClean>>();
                //entity.Del<TimerDoneEvent<TimerForPopClean>>();
                entity.Del<CleanIt>();
            }
        }
    }
}