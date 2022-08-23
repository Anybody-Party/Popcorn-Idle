using Leopotam.Ecs;
using UnityEngine;

namespace Client
{
    public class PopViewSystem : IEcsRunSystem
    {
        private GameData _gameData;
        private GameUI _gameUi;
        private EcsWorld _world;

        private EcsFilter<Pop, ChangePopViewRequest> _filter;
        private EcsFilter<PopcornViewLink, RandomizePopRotationViewRequest> _rotationfilter;

        public void Run()
        {
            foreach (var idx in _filter)
            {
                ref EcsEntity entity = ref _filter.GetEntity(idx);
                ref PopcornViewLink popView = ref entity.Get<PopcornViewLink>();
                ref ChangePopViewRequest changePopViewRequest = ref entity.Get<ChangePopViewRequest>();

                if (changePopViewRequest.PopBodyView ==  PopBodyView.Popcorn)
                {
                    popView.DoneBody.SetActive(true);
                    popView.BaseBody.SetActive(true);
                    popView.RawBody.SetActive(false);
                }

                if (changePopViewRequest.PopBodyView == PopBodyView.PopcornWithoutLimbs)
                {
                    popView.DoneBody.SetActive(true);
                    popView.BaseBody.SetActive(false);
                    popView.RawBody.SetActive(false);
                }

                if (changePopViewRequest.PopBodyView == PopBodyView.RawCorn)
                {
                    popView.DoneBody.SetActive(false);
                    popView.BaseBody.SetActive(false);
                    popView.RawBody.SetActive(true);
                }

                entity.Del<ChangePopViewRequest>();
            }

            foreach (var idx in _rotationfilter)
            {
                ref EcsEntity entity = ref _rotationfilter.GetEntity(idx);
                
                _rotationfilter.Get1(idx).DoneBody.transform.Rotate(new Vector3(Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f)), Random.Range(0, 360.0f));

                entity.Del<RandomizePopRotationViewRequest>();
            }
        }
    }
}