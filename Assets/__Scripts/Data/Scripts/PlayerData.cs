using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayerData : BaseData
{
    public double Money;
    public double MoneyInSec;
    public int GoldPopcornAmount;
    public int PopcornAmount;

    public int CurrentLevelIndex;
    public float CurrentLevelProgress;

    [Header("Heating Upgrades")]
    public int HeatingMaxTemperatureUpgradeLevel;
    public int HeatingSpeedUpgradeLevel;
    public int HeatingAreaUpgradeLevel;

    [Header("Conveyor Upgrades")]
    public int LaunchSpeedUpgradeLevel;
    public int ConveyorSpeedUpgradeLevel;
    public int BagSizeUpgradeLevel;

    [Header("Earning Upgrades")]
    public int EarnForPopUpgradeLevel;
    public int EarnForBagUpgradeLevel;
    public int EarnOfflineUpgradeLevel;

    //[Header("Boosters")] // save it for reload

    public List<int> ConveyorLevels;
    public List<int> ProductLineLevels;

    public override void ResetData()
    {
        Money = 0;
        GoldPopcornAmount = 0;
        PopcornAmount = 0;

        CurrentLevelIndex = 0;
        CurrentLevelProgress = 0.0f;

        HeatingMaxTemperatureUpgradeLevel = 1;
        HeatingSpeedUpgradeLevel = 1;
        HeatingAreaUpgradeLevel = 1;

        LaunchSpeedUpgradeLevel = 1;
        ConveyorSpeedUpgradeLevel = 1;
        BagSizeUpgradeLevel = 1;

        EarnForPopUpgradeLevel = 1;
        EarnForBagUpgradeLevel = 1;
        EarnOfflineUpgradeLevel = 1;

        for (int i = 0; i < ConveyorLevels.Count; i++)
            ConveyorLevels[i] = 0;
        ConveyorLevels[0] = 1;

        for (int i = 0; i < ProductLineLevels.Count; i++)
            ProductLineLevels[i] = 0;
        ProductLineLevels[0] = 1;
    }

    public void Init()
    {
        ConveyorLevels = new List<int>();
        for (int i = 0; i < 10; i++)
            ConveyorLevels.Add(0);
        ConveyorLevels[0] = 1;
    }
}
