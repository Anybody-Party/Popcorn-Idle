using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayerData : BaseData
{
    public double Money;
    public int GoldPopcornAmount;
    public int PopcornAmount;

    public int CurrentLevelIndex;
    public float CurrentLevelProgress;

    public List<int> ConveyorLevels;

    public override void ResetData()
    {
        Money = 0;
        GoldPopcornAmount = 0;
        PopcornAmount = 0;

        CurrentLevelIndex = 0;
        CurrentLevelProgress = 0.0f;

        for (int i = 0; i < ConveyorLevels.Count; i++)
            ConveyorLevels[i] = 0;
        ConveyorLevels[0] = 1;
    }

    public void Init()
    {
        ConveyorLevels = new List<int>();
        for (int i = 0; i < 10; i++)
            ConveyorLevels.Add(0);
        ConveyorLevels[0] = 1;
    }
}
