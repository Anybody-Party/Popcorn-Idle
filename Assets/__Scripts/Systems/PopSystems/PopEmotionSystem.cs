using Leopotam.Ecs;

namespace Client
{
    public class PopEmotionSystem : IEcsRunSystem
    {
        private GameData _gameData;
        private GameUI _gameUi;
        private EcsWorld _world;

        private EcsFilter<Pop, SetPopEmotionEvent> _filter;

        public void Run()
        {
            foreach (var idx in _filter)
            {
                ref EcsEntity entity = ref _filter.GetEntity(idx);
                ref PopcornViewLink popView = ref entity.Get<PopcornViewLink>();

                if (entity.Has<PopCookingDoneEvent>())
                {
                    popView.SmileEmotion.SetActive(true);
                    popView.HappyEmotion.SetActive(false);
                    popView.ScaryEmotion.SetActive(false);
                }

                if (entity.Has<DespawnTag>())
                {
                    popView.SmileEmotion.SetActive(true);
                    popView.HappyEmotion.SetActive(false);
                    popView.ScaryEmotion.SetActive(false);
                }

                if (entity.Has<InJump>())
                {
                    popView.SmileEmotion.SetActive(false);
                    popView.HappyEmotion.SetActive(true);
                    popView.ScaryEmotion.SetActive(false);
                }

                entity.Del<SetPopEmotionEvent>();
            }
        }
    }
}