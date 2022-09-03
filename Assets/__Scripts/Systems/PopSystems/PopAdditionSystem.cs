﻿using Leopotam.Ecs;

namespace Client
{
    public class PopAdditionSystem : IEcsRunSystem
    {
        private GameData _gameData;
        private GameUI _gameUi;
        private EcsWorld _world;

        private EcsFilter<Pop, ChangePopAdditionRequest> _filter;

        public void Run()
        {
            foreach (var idx in _filter)
            {
                ref EcsEntity entity = ref _filter.GetEntity(idx);
                ref PopcornViewLink popView = ref entity.Get<PopcornViewLink>();
                ref ChangePopAdditionRequest changePopAdditionRequest = ref entity.Get<ChangePopAdditionRequest>();

                popView.ChocolateAddition.SetActive(false);
                popView.SaltAddition.SetActive(false);
                popView.CaramelAddition.SetActive(false);
                popView.WasabiAddition.SetActive(false);

                if (changePopAdditionRequest.Addition == PopAdditions.Chocolate)
                    popView.ChocolateAddition.SetActive(true);

                if (changePopAdditionRequest.Addition == PopAdditions.Salt)
                    popView.SaltAddition.SetActive(true);

                if (changePopAdditionRequest.Addition == PopAdditions.Caramel)
                    popView.CaramelAddition.SetActive(true);

                if (changePopAdditionRequest.Addition == PopAdditions.Wasabi)
                    popView.WasabiAddition.SetActive(true);

                entity.Del<ChangePopAdditionRequest>();
            }
        }
    }
}