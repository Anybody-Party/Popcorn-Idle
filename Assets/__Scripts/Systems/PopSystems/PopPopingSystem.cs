using Leopotam.Ecs;
using UnityEngine;

namespace Client
{
    public class PopPopingSystem : IEcsRunSystem
    {
        private GameData _gameData;
        private GameUI _gameUi;
        private EcsWorld _world;

        private EcsFilter<Pop, Poping> _filter;

        public void Run()
        {
            foreach (var idx in _filter)
            {
                ref EcsEntity entity = ref _filter.GetEntity(idx);
                ref Pop pop = ref entity.Get<Pop>();
                ref PopcornViewLink popView = ref entity.Get<PopcornViewLink>();
                ref GameObjectLink entityGo = ref entity.Get<GameObjectLink>();
                ref RigidbodyLink entityRb = ref entity.Get<RigidbodyLink>();

                popView.DoneBody.SetActive(true);
                entityGo.Value.transform.rotation = Quaternion.identity;
                entityRb.Value.freezeRotation = true;

                Vector3 randomDirection = Random.onUnitSphere;
                Debug.Log(randomDirection);
                entity.Get<AddingForce>() = new AddingForce
                {
                    Direction = new Vector3(Mathf.Abs(randomDirection.x), Mathf.Abs(randomDirection.y) * _gameData.StaticData.PopingPopcornForce, Mathf.Abs(randomDirection.z)),
                    ForceMode = ForceMode.Impulse
                };

                entity.Del<Poping>();
                entity.Get<Landing>();
                entity.Get<DelayTimer>().Value = 3.0f;
                entity.Get<GoToJump>();
            }
        }
    }
}