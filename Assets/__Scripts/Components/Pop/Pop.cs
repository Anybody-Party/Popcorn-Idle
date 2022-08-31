using System;
using Leopotam.Ecs;
using UnityEngine;

namespace Client
{
    internal struct Pop
    {
        public ConveyorLink Conveyor;
        public int ConveyorId;
        public int ProductLineId;
    }

    public enum PopAnimations
    {
        IsWalking,
        IsRunning,
        IsPop,
        IsPrepareToJump,
        IsJump,
        IsMissFalling,
        IsFalling,
        IsGoldTaken,
        WalkingIndex,
        JumpIndex
    }

    public enum PopEmotions
    {
        Empty,
        Smile,
        Happy,
        Scary
    }

    public enum PopBodyView
    {
        RawCorn,
        Popcorn,
        PopcornWithoutLimbs
    }

    public enum PopAdditions
    {
        None,
        Chocolate,
        Salt,
        Caramel,
        Wasabi
    }

    public static class PopExtensions
    {
        public static void StopAllMoving(ref EcsEntity pop)
        {
            pop.Del<VelocityMoving>();
            pop.Del<TransformMoving>();
            pop.Del<LookingAt>();
            pop.Del<GoToJump>();
        }

        public static void PrepareToDespawn(ref EcsEntity pop)
        {
            pop.Get<DespawnTag>();
            pop.Get<ChangePopViewRequest>().PopBodyView = PopBodyView.RawCorn;
            pop.Get<ChangePopEmotionRequest>().Emotion = PopEmotions.Empty;
            pop.Get<ChangePopAdditionRequest>().Addition = PopAdditions.None;
            pop.Get<RigidbodyLink>().Value.isKinematic = false;
        }
    }
}