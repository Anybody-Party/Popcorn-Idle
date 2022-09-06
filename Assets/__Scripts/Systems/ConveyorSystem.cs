using Leopotam.Ecs;
using UnityEngine;

namespace Client
{
    public class ConveyorSystem : IEcsInitSystem, IEcsRunSystem
    {
        private GameData _gameData;
        private GameUI _gameUi;
        private EcsWorld _world;
        private WorldGameUI _worldGameUi;

        private EcsFilter<ConveyorLink> _filter;
        private EcsFilter<BuyConveyorRequest> _buyFilter;

        public void Init()
        {
            foreach (var idx in _filter)
            {
                ref EcsEntity entity = ref _filter.GetEntity(idx);

                ref ConveyorLink conveyor = ref entity.Get<ConveyorLink>();
                ref GameObjectLink entityGo = ref entity.Get<GameObjectLink>();

                for (int i = 0; i < _gameData.SceneData.Conveyors.Count; i++)
                    if (conveyor.Id == i)
                        _filter.Get1(idx).IsBuyed = _gameData.PlayerData.ConveyorBuyed[i];

                if (conveyor.IsBuyed)
                {
                    entity.Get<LaunchPop>();
                    entityGo.Value.SetActive(true);
                }
                else
                {
                    entityGo.Value.SetActive(false);
                }
            }

            SetZoomForConveyors();
            _worldGameUi.UpdateBuyConveyorScreens();
        }

        public void Run()
        {
            _worldGameUi.UpdateBuyConveyorScreens(); //TODO: Remove

            foreach (var buy in _buyFilter)
            {
                ref EcsEntity buyEventEntity = ref _buyFilter.GetEntity(buy);
                ref BuyConveyorRequest buyRequest = ref buyEventEntity.Get<BuyConveyorRequest>();

                foreach (var idx in _filter)
                {
                    ref EcsEntity entity = ref _filter.GetEntity(idx);

                    ref ConveyorLink conveyor = ref entity.Get<ConveyorLink>();
                    ref GameObjectLink entityGo = ref entity.Get<GameObjectLink>();

                    for (int i = 0; i < _gameData.SceneData.Conveyors.Count; i++)
                        if (conveyor.Id == buyRequest.ConveyorId)
                        {
                            conveyor.IsBuyed = true;
                            _gameData.PlayerData.ConveyorBuyed[conveyor.Id] = conveyor.IsBuyed;
                            double price = GameData.Instance.BalanceData.BaseConveyorPrice * Mathf.Pow(GameData.Instance.BalanceData.ConveyorPriceMultiplierForNumber, buyRequest.ConveyorId);
                            _world.NewEntity().Get<SpendMoneyEvent>().Value = price;
                            conveyor.BuildDustPS.Play();
                            _world.NewEntity().Get<PlaySoundRequest>().SoundName = StaticData.AudioSound.BuildNewConveyorSound;
                            entityGo.Value.SetActive(true);
                            entity.Get<LaunchPop>();
                        }
                }
                SetZoomForConveyors();
                buyEventEntity.Del<BuyConveyorRequest>();
            }
        }

        private void SetZoomForConveyors()
        {
            int counter = 0;
            for (int i = 0; i < _gameData.PlayerData.ConveyorBuyed.Count; i++)
                if (_gameData.PlayerData.ConveyorBuyed[i])
                    counter++;

            _world.NewEntity().Get<SetCameraZoomReqeust>().ZoomObjects = counter;
        }
    }
}