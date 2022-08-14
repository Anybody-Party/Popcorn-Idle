using System;
using System.Collections.Generic;

[Serializable]
public class PlayerData : BaseData
{
    public int CurrentLevelIndex;
    public float CurrentLevelProgress;
    public List<int> ConveyorLevels;

    public double Money;
    public int GoldPopcornAmount;
    public int PopcornAmount;

    public override void ResetData()
    {
        CurrentLevelIndex = 0;
        CurrentLevelProgress = 0.0f;

        ConveyorLevels = new List<int>(10);
        for (int i = 0; i < ConveyorLevels.Count; i++)
            ConveyorLevels[i] = 0;
    }
}
