using Leopotam.Ecs;
using UnityEngine;

namespace Client
{
    public partial class PopAnimationSystem : IEcsRunSystem
    {
        private EcsFilter<Pop, AnimatorLink, ChangeAnimationRequest> _filter;

        public void Run()
        {
            foreach (var idx in _filter)
            {
                ref EcsEntity entity = ref _filter.GetEntity(idx);
                ref AnimatorLink entityAnimator = ref entity.Get<AnimatorLink>();
                ref ChangeAnimationRequest changeAnimationAction = ref entity.Get<ChangeAnimationRequest>();

                if (changeAnimationAction.Animation == PopAnimations.IsPop)
                    SetAnimation(entityAnimator.Value, PopAnimations.IsPop.ToString());

                if (changeAnimationAction.Animation == PopAnimations.IsWalking)
                {
                    int walkRandom = Random.Range(0, 2);
                    SetAnimation(entityAnimator.Value, PopAnimations.IsWalking.ToString(), walkRandom, PopAnimations.WalkingIndex.ToString());
                }

                if (changeAnimationAction.Animation == PopAnimations.IsRunning)
                    SetAnimation(entityAnimator.Value, PopAnimations.IsRunning.ToString());

                if (changeAnimationAction.Animation == PopAnimations.IsPrepareToJump)
                    SetAnimation(entityAnimator.Value, PopAnimations.IsPrepareToJump.ToString());

                if (changeAnimationAction.Animation == PopAnimations.IsJump)
                {
                    int jumpRandom = Random.Range(0, 3);
                    SetAnimation(entityAnimator.Value, PopAnimations.IsJump.ToString(), jumpRandom, PopAnimations.JumpIndex.ToString());
                }

                //if (entity.Has<Landing>())
                //    SetAnimation(entityAnimator.Value, PopAnimations.IsMissFalling.ToString());

                //if (entity.Has<Landing>())
                //    SetAnimation(entityAnimator.Value, PopAnimations.IsFalling.ToString());

                entity.Del<ChangeAnimationRequest>();
            }
        }

        private void SetAnimation(Animator animator, string animation, int randomAnimation = -1, string animationRandom = null)
        {
            if (animator.GetBool(animation))
                return;
            Utility.ResetAnimtor(animator);

            if(animationRandom != null)
                animator.SetInteger(animationRandom, randomAnimation);

            animator.SetTrigger(animation);
        }
    }
}