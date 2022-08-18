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

                if (heroEntity.Has<TransformMoving>() && heroEntity.Has<GoToJump>())
                    SetAnimation(heroAnimator.Value, PopAnimations.IsWalking.ToString());

                if (heroEntity.Has<IsSpeedUp>())
                    SetAnimation(heroAnimator.Value, PopAnimations.IsRunning.ToString());

                if (heroEntity.Has<ReadyToJump>())
                    SetAnimation(heroAnimator.Value, PopAnimations.IsPrepareToJump.ToString());

                if (heroEntity.Has<InJump>())
                    SetAnimation(heroAnimator.Value, PopAnimations.IsJump.ToString());

                if (heroEntity.Has<Landing>())
                    SetAnimation(heroAnimator.Value, PopAnimations.IsMissFalling.ToString());

                if (heroEntity.Has<Landing>())
                    SetAnimation(heroAnimator.Value, PopAnimations.IsFalling.ToString());

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