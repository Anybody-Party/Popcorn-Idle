using Leopotam.Ecs;

namespace Client
{
    public class HandSystem : IEcsRunSystem
    {
        private GameData _gameData;
        private GameUI _gameUi;
        private EcsWorld _world;

        private EcsFilter<HandTakenRequest> _takenfilter;
        private EcsFilter<HandLink> _handsFilter;
        private EcsFilter<ShakeBagRequest> _shakeFilter;

        public void Run()
        {
            foreach (var idx in _takenfilter)
            {
                ref EcsEntity entity = ref _takenfilter.GetEntity(idx);
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

            foreach (var idx in _shakeFilter)
            {
                ref EcsEntity entity = ref _shakeFilter.GetEntity(idx);
                ref ShakeBagRequest shakeBagRequest = ref entity.Get<ShakeBagRequest>();

                foreach (var hand in _handsFilter)
                {
                    ref EcsEntity handEntity = ref _handsFilter.GetEntity(hand);
                    ref HandLink handLink = ref handEntity.Get<HandLink>();

                    if (shakeBagRequest.ProductLineId == handLink.ProductLineId)
                    {
                        ref AnimatorLink animatorLink = ref handEntity.Get<AnimatorLink>();
                        animatorLink.Value.SetTrigger("IsPopIn");

                        entity.Del<ShakeBagRequest>();
                    }
                }
            }
        }
    }
}