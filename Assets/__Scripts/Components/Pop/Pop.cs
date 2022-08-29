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
}