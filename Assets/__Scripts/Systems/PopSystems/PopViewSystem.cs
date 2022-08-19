using Leopotam.Ecs;

namespace Client
{
    public class PopViewSystem : IEcsRunSystem
    {
        private GameData _gameData;
        private GameUI _gameUi;
        private EcsWorld _world;

        private EcsFilter<Pop, SetPopViewEvent> _filter;

        public void Run()
        {
            foreach (var idx in _filter)
            {
                ref EcsEntity entity = ref _filter.GetEntity(idx);
                ref PopcornViewLink popView = ref entity.Get<PopcornViewLink>();

                if (entity.Has<PopCookingDoneEvent>())
                {
                    popView.DoneBody.SetActive(true);
                    popView.BaseBody.SetActive(true);
                    popView.RawBody.SetActive(false);
                }

                if (entity.Has<DespawnTag>())
                {
                    popView.DoneBody.SetActive(false);
                    popView.RawBody.SetActive(true);
                }

                if (entity.Has<ReadyToSell>())
                {
                    popView.BaseBody.SetActive(false);
                }

                entity.Del<SetPopViewEvent>();
            }
        }
    }
}