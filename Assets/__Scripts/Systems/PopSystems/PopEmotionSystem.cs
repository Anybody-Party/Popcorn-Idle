using Leopotam.Ecs;

namespace Client
{
    public class PopEmotionSystem : IEcsRunSystem
    {
        private GameData _gameData;
        private GameUI _gameUi;
        private EcsWorld _world;

        private EcsFilter<Pop, ChangePopEmotionRequest> _filter;

        public void Run()
        {
            foreach (var idx in _filter)
            {
                ref EcsEntity entity = ref _filter.GetEntity(idx);
                ref PopcornViewLink popView = ref entity.Get<PopcornViewLink>();
                ref ChangePopEmotionRequest changePopEmotionRequest = ref entity.Get<ChangePopEmotionRequest>();

                popView.SmileEmotion.SetActive(false);
                popView.HappyEmotion.SetActive(false);
                popView.ScaryEmotion.SetActive(false);

                if (changePopEmotionRequest.Emotion == PopEmotions.Smile)
                    popView.SmileEmotion.SetActive(true);

                if (changePopEmotionRequest.Emotion == PopEmotions.Happy)
                    popView.HappyEmotion.SetActive(true);

                if (changePopEmotionRequest.Emotion == PopEmotions.Scary)
                    popView.ScaryEmotion.SetActive(true);

                entity.Del<ChangePopEmotionRequest>();
            }
        }
    }
}