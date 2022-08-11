using Leopotam.Ecs;
using UnityEngine;

namespace Client
{
    public class LookAtSystem : IEcsRunSystem
    {
        private EcsFilter<LookingAt> _lookAtFilter;

        public void Run()
        {
            foreach (var lookAtObject in _lookAtFilter)
            {
                ref EcsEntity lookAtEntity = ref _lookAtFilter.GetEntity(lookAtObject);
                ref GameObjectLink lookAtEntityGo = ref lookAtEntity.Get<GameObjectLink>();
                ref RigidbodyLink lookAtEntityRb = ref lookAtEntity.Get<RigidbodyLink>();

                ref LookingAt lookAtTargetPosition = ref lookAtEntity.Get<LookingAt>();

                var direction = Quaternion.LookRotation((lookAtTargetPosition.Value - lookAtEntityGo.Value.transform.position)).normalized;
                lookAtEntityRb.Value.MoveRotation(direction);

                if (ApproximatelyQuaternions(lookAtEntityRb.Value.rotation, direction, 0.1f))
                {
                    lookAtEntity.Del<LookingAt>();
                }
            }
        }

        private bool ApproximatelyQuaternions(Quaternion quatA, Quaternion value, float acceptableRange)
        {
            return 1 - Mathf.Abs(Quaternion.Dot(quatA, value)) < acceptableRange;
        }
    }
}