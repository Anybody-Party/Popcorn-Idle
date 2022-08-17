using Leopotam.Ecs;

namespace Client
{
    public class PopViewHandlerSystem : IEcsRunSystem
    {
        private GameData _gameData;
        private GameUI _gameUi;
        private EcsWorld _world;

        private EcsFilter<Pop, PopCookingDoneEvent> _doneFilter;

        public void Run()
        {
            foreach (var idx in _doneFilter)
            {
                ref EcsEntity entity = ref _doneFilter.GetEntity(idx);
                ref PopcornViewLink popView = ref entity.Get<PopcornViewLink>();

                popView.DoneBody.SetActive(true);
                popView.RawBody.SetActive(false);
            }
        }
    }
}