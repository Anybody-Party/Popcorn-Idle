using Leopotam.Ecs;

namespace Client
{
    public class HandSystem : IEcsRunSystem
    {
        private GameData _gameData;
        private GameUI _gameUi;
        private EcsWorld _world;

        private EcsFilter<HandTakenRequest> _filter;
        private EcsFilter<HandLink> _handsFilter;

        public void Run()
        {
            foreach (var idx in _filter)
            {
                ref EcsEntity entity = ref _filter.GetEntity(idx);
                ref HandTakenRequest requset = ref entity.Get<HandTakenRequest>();

                foreach (var hand in _handsFilter)
                {
                    ref EcsEntity handEntity = ref _handsFilter.GetEntity(hand);
                    ref HandLink handLink = ref handEntity.Get<HandLink>();
                    
                    if (requset.ProductLineId == handLink.ProductLineId)
                    {
                        ref AnimatorLink animatorLink = ref handEntity.Get<AnimatorLink>();
                        animatorLink.Value.SetTrigger("IsTaken");

                        double reward = _gameData.RuntimeData.GetBagEarning(); // TODO: Add additions modificator

                        _world.NewEntity().Get<EarnMoneyEvent>().Value = reward;

                        EcsEntity earnViewEntity = _world.NewEntity();
                        earnViewEntity.Get<EarnView>() = new EarnView
                        {
                            Value = reward,
                            Position = handLink.EarnMoneyPS.transform.position
                        };
                        handLink.EarnMoneyPS.Play();

                        entity.Del<HandTakenRequest>();
                    }
                }
            }
        }
    }
}