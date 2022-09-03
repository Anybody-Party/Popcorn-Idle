using DG.Tweening;
using Leopotam.Ecs;
using UnityEngine;

namespace Client
{
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

                        if (popGo.Value == raycast.GameObject)
                        {
                            popView.SpeedUpTrail.SetActive(true);

                            popGo.Value.transform.DOMove(_gameData.SceneData.CameraController.GetComponent<Camera>().ScreenToWorldPoint(_gameUi.GameScreen.GetGoldPopPosition()), _gameData.BalanceData.BaseGetGoldPopTime).SetEase(Ease.InCirc);
                            popGo.Value.transform.DOScale(Vector3.zero, _gameData.BalanceData.BaseGetGoldPopTime * 3f);
                            entity.Get<IsThisGoldTakenMarker>();
                            entity.Get<Timer<TimerGoldTaken>>().Value = _gameData.BalanceData.BaseGetGoldPopTime;
                            entity.Get<ChangeAnimationRequest>().Animation = PopAnimations.IsGoldTaken;
                            entity.Get<ChangePopEmotionRequest>().Emotion = PopEmotions.Happy;
                        }
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