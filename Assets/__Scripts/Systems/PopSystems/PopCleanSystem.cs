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

                _filter.GetEntity(idx).Get<Timer<TimerForPopClean>>().Value = _gameData.BalanceData.CleanTime;
                entityGo.Value.transform.DOScale(Vector3.zero, _gameData.BalanceData.CleanTime).SetEase(Ease.InOutBounce);
            }

            foreach (var idx in _timerDoneFilter)
            {
                ref EcsEntity entity = ref _timerDoneFilter.GetEntity(idx);
                ref GameObjectLink entityGo = ref entity.Get<GameObjectLink>();

                entity.Get<DespawnTag>();
                entityGo.Value.transform.localScale = Vector3.one * 0.1f;
                entity.Get<ChangePopViewRequest>().PopBodyView = PopBodyView.RawCorn;
                entity.Get<ChangePopEmotionRequest>().Emotion = PopEmotions.Empty;
                entity.Del<TimerForPopClean>();
                entity.Del<CleanIt>();
            }
        }
    }
}