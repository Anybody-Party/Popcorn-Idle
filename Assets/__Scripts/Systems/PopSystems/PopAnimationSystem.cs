using Leopotam.Ecs;
using UnityEngine;

namespace Client
{
    public partial class PopAnimationSystem : IEcsRunSystem
    {
        private EcsFilter<Pop, AnimatorLink, SetAnimationEvent> _heroAnimationFilter;

        public void Run()
        {
            foreach (var hero in _heroAnimationFilter)
            {
                ref EcsEntity heroEntity = ref _heroAnimationFilter.GetEntity(hero);
                ref AnimatorLink heroAnimator = ref heroEntity.Get<AnimatorLink>();

                if (heroEntity.Has<VelocityMoving>())
                    SetAnimation(heroAnimator.Value, PopAnimations.IsRunning.ToString());

                heroEntity.Del<SetAnimationEvent>();
            }
        }

        private void SetAnimation(Animator animator, string animation)
        {
            if (animator.GetBool(animation))
                return;
            Utility.ResetAnimtor(animator);
            animator.SetTrigger(animation);
        }
    }
}