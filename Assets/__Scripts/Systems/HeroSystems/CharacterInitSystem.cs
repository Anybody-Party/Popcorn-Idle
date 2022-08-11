using Leopotam.Ecs;
using UnityEngine;

namespace Client
{
    public class CharacterInitSystem : IEcsRunSystem
    {
        private GameData _gameData;

        private EcsFilter<Character> _filter;

        public void Run()
        {
            foreach (var idx in _filter)
            {
                ref EcsEntity entity = ref _filter.GetEntity(idx);

            }
        }
    }
}