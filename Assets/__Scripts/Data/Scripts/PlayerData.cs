using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "GameData/PlayerData", fileName = "PlayerData")]
public class PlayerData : BaseDataSO
{
    public double Money;
    public double MoneyInSec;
    public int GoldPopcornAmount;
    public int PopcornAmount;

    public int CurrentLevelIndex;
    public float CurrentLevelProgress;

    [Header("Heating Upgrades")]
    public UpgradeLevel HeatingMaxTemperatureUpgrade;
    public int HeatingMinTemperatureUpgradeLevel;
    public int HeatingSpeedUpgradeLevel;

    [Header("Conveyor Upgrades")]
    public int LaunchSpeedUpgradeLevel;
    public int ConveyorSpeedUpgradeLevel;
    public int BagSizeUpgradeLevel;

    [Header("Earning Upgrades")]
    public int EarnForPopUpgradeLevel;
    public int EarnForBagUpgradeLevel;
    public int EarnOfflineUpgradeLevel;

    [Header("Level Upgardes")]
    public List<UpgradeLevel> CommonUpgradeLevels;
    public List<UpgradeLevel> EpicUpgradeLevels;

    //[Header("Boosters")] // save it for reload

    public List<bool> ConveyorBuyed;
    public List<bool> ProductLineBuyed;

    public override void ResetData()
    {
        Money = 0;
        GoldPopcornAmount = 0;
        PopcornAmount = 0;

        CurrentLevelIndex = 0;
        CurrentLevelProgress = 0.0f;

        HeatingSpeedUpgradeLevel = 1;
        HeatingMinTemperatureUpgradeLevel = 1;

        LaunchSpeedUpgradeLevel = 1;
        ConveyorSpeedUpgradeLevel = 1;
        BagSizeUpgradeLevel = 1;

        EarnForPopUpgradeLevel = 1;
        EarnForBagUpgradeLevel = 1;
        EarnOfflineUpgradeLevel = 1;
    }

    public void Init()
    {
        for (int i = 0; i < 9; i++)
            CommonUpgradeLevels[i].Level = 1;

        for (int i = 0; i < 3; i++)
            EpicUpgradeLevels[i].Level = 1;

        foreach (var itemIn in GameData.Instance.BalanceData.CommonUpgradeData)
            foreach (var itemOut in CommonUpgradeLevels)
                if (itemIn.UpgradeKey == itemOut.UpgradeKey)
                    itemIn.Level = itemOut.Level;

        for (int i = 0; i < 5; i++)
            ConveyorBuyed[i] = false;
        ConveyorBuyed[0] = true;

        for (int i = 0; i < 3; i++)
            ProductLineBuyed[i] = false;
        ProductLineBuyed[0] = true;
    }
}

[Serializable]
public class UpgradeLevel
{
    public string UpgradeKey;
    public int Level;
}