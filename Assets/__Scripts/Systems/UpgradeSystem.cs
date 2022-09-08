using Leopotam.Ecs;
using UnityEngine;

namespace Client
{
    public class UpgradeSystem : IEcsRunSystem
    {
        private GameData _gameData;
        private GameUI _gameUi;
        private EcsWorld _world;

        private EcsFilter<UpgradeEvent> _filter;

        public void Run()
        {
            foreach (var idx in _filter)
            {
                ref EcsEntity entity = ref _filter.GetEntity(idx);
                ref UpgradeEvent upgrade = ref entity.Get<UpgradeEvent>();

                foreach (var item in GameData.Instance.PlayerData.CommonUpgradeLevels)
                    if (item.UpgradeKey == upgrade.Key)
                        item.Level = upgrade.Level;

                foreach (var item in GameData.Instance.PlayerData.EpicUpgradeLevels)
                    if (item.UpgradeKey == upgrade.Key)
                        item.Level = upgrade.Level;

                GameData.Instance.PlayerData.UpdateUpgradeDataLevel();
                _world.NewEntity().Get<PlaySoundRequest>().SoundName = StaticData.AudioSound.BuyUpdateSound;
                entity.Del<UpgradeEvent>();
            }
        }
    }
}