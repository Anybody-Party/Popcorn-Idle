using System;
using System.Collections.Generic;

[Serializable]
public class PlayerData : BaseData
{
    public int CurrentLevelIndex;
    public float CurrentLevelProgress;
    public List<int> ConveyorLevels;

    public double Money;

    public override void ResetData()
    {
    }
}
