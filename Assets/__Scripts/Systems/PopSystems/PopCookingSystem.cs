using Leopotam.Ecs;
using UnityEngine;

namespace Client
{
    public class PopCookingSystem : IEcsRunSystem
    {
        private GameData _gameData;
        private GameUI _gameUi;
        private EcsWorld _world;

        private EcsFilter<Pop, Cooking>.Exclude<Done, Landing> _filter;

        public void Run()
        {
            foreach (var idx in _filter)
            {
                ref EcsEntity entity = ref _filter.GetEntity(idx);
                ref DoneTimer doneTimer = ref entity.Get<DoneTimer>();
                doneTimer.Value += Time.deltaTime * _gameData.RuntimeData.Temperature * 0.1f;

                if (doneTimer.Value > _gameData.BalanceData.BaseCookingTime)
                {
                    entity.Del<Cooking>();
                    entity.Get<Done>();
                    entity.Get<PopCookingDoneEvent>();
                    entity.Get<ChangePopViewRequest>().PopBodyView = PopBodyView.Popcorn;
                    entity.Del<DoneTimer>();
                    entity.Get<Poping>();
                }
            }
        }
    }
}