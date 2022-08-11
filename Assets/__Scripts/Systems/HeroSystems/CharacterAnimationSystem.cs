using Leopotam.Ecs;
using UnityEngine;

namespace Client
{
    public partial class CharacterAnimationSystem : IEcsRunSystem
    {
        private EcsFilter<Character, AnimatorLink, SetAnimationEvent> _heroAnimationFilter;

        public void Run()
        {
            foreach (var hero in _heroAnimationFilter)
            {
                ref EcsEntity heroEntity = ref _heroAnimationFilter.GetEntity(hero);
                ref AnimatorLink heroAnimator = ref heroEntity.Get<AnimatorLink>();

                if (heroEntity.Has<Moving>())
                    SetAnimation(heroAnimator.Value, CharacterAnimations.IsRunning.ToString());

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