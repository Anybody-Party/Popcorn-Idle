using Leopotam.Ecs;
using UnityEngine;

namespace Client
{
    public partial class PopAnimationSystem : IEcsRunSystem
    {
        private EcsFilter<Pop, AnimatorLink, SetAnimationEvent> _filter;

        public void Run()
        {
            foreach (var idx in _filter)
            {
                ref EcsEntity entity = ref _filter.GetEntity(idx);
                ref AnimatorLink entityAnimator = ref entity.Get<AnimatorLink>();

                if (entity.Has<Landing>() && entity.Has<GoToJump>())
                    SetAnimation(entityAnimator.Value, PopAnimations.IsPop.ToString());

                if (entity.Has<TransformMoving>() && entity.Has<GoToJump>())
                    SetAnimation(entityAnimator.Value, PopAnimations.IsWalking.ToString());

                if (entity.Has<IsSpeedUp>())
                    SetAnimation(entityAnimator.Value, PopAnimations.IsRunning.ToString());

                if (entity.Has<ReadyToJump>())
                    SetAnimation(entityAnimator.Value, PopAnimations.IsPrepareToJump.ToString());

                if (entity.Has<InJump>())
                    SetAnimation(entityAnimator.Value, PopAnimations.IsJump.ToString());

                //if (entity.Has<Landing>())
                //    SetAnimation(entityAnimator.Value, PopAnimations.IsMissFalling.ToString());

                //if (entity.Has<Landing>())
                //    SetAnimation(entityAnimator.Value, PopAnimations.IsFalling.ToString());

                entity.Del<SetAnimationEvent>();
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