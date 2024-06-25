using Leopotam.Ecs;
using UnityEngine;

namespace Client
{
    public class LevelProgressSystem : IEcsRunSystem, IEcsInitSystem
    {
        private EcsWorld _world;
        private GameData _gameData;
        private GameUI _gameUi;
        private AdsManager _adsManager;
        
        private EcsFilter<GetAdLevelCompleteRewardRequest> _adRewardFilter;
        private EcsFilter<AdLevelRewardCompleteRequest> _adCompleteFilter;
        private EcsFilter<GetLevelCompleteRewardRequest> _rewardFilter;
        private EcsFilter<AddExpRequest> _expFilter;

        public void Init()
        {
            _gameUi.GameScreen.UpdateLevelText(_gameData.PlayerData.Level);
            _gameUi.GameScreen.UpdateProgressBar(_gameData.PlayerData.LevelProgress / _gameData.RuntimeData.GetNeededExp());
        }

        public void Run()
        {
            foreach (var idx in _adRewardFilter)
            {
                ref EcsEntity entity = ref _adRewardFilter.GetEntity(idx);
                _adsManager.ShowRewarded();
                entity.Del<GetAdLevelCompleteRewardRequest>();
            }
            
            foreach (var idx in _adCompleteFilter)
            {
                ref EcsEntity entity = ref _adCompleteFilter.GetEntity(idx);
                _world.NewEntity().Get<EarnMoneyEvent>().Value = _gameData.RuntimeData.GetLevelCompleteReward() * 3.0f;
                entity.Del<AdLevelRewardCompleteRequest>();
            }
            
            foreach (var idx in _rewardFilter)
            {
                ref EcsEntity entity = ref _rewardFilter.GetEntity(idx);
                _adsManager.ShowInterstitial();
                _world.NewEntity().Get<EarnMoneyEvent>().Value = _gameData.RuntimeData.GetLevelCompleteReward();
                entity.Del<GetLevelCompleteRewardRequest>();
            }
            
            foreach (var idx in _expFilter)
            {
                ref EcsEntity entity = ref _expFilter.GetEntity(idx);
                _gameData.PlayerData.LevelProgress += 1;
                Debug.Log($"_gameData.PlayerData.LevelProgress {_gameData.PlayerData.Level}");
                _gameUi.GameScreen.UpdateProgressBar(_gameData.PlayerData.LevelProgress / _gameData.RuntimeData.GetNeededExp());
                if (_gameData.PlayerData.LevelProgress >= _gameData.RuntimeData.GetNeededExp())
                {
                    _gameData.PlayerData.LevelProgress = 0.0f;
                    _gameData.PlayerData.Level += 1;
                    _gameUi.GameScreen.UpdateProgressBar(0.0f);
                    _gameUi.GameScreen.UpdateLevelText(_gameData.PlayerData.Level);
                    _gameUi.LevelCompleteScreen.UpdateRewardText(_gameData.RuntimeData.GetLevelCompleteReward());
                    _gameUi.SetShowStateLevelCompleteScreen(true);
                }
                
                entity.Del<AddExpRequest>();
            }
        }
    }

    public struct AdLevelRewardCompleteRequest
    {
    }

    public struct AddExpRequest : IEcsIgnoreInFilter
    {
    }

    public struct GetLevelCompleteRewardRequest
    {
    }

    public struct GetAdLevelCompleteRewardRequest : IEcsIgnoreInFilter
    {
    }
}
