using System;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

[CreateAssetMenu(menuName = "GameData/PlayerData", fileName = "PlayerData")]
public class PlayerData : BaseDataSO
{
    public double Money;
    public double MoneyInSec;
    public double GoldPopcornAmount;
    public double PopcornAmount;

    public int CurrentLevelIndex;
    public float CurrentLevelProgress;
    
    [Header("Heating"), BoxGroup("Common")] public UpgradeLevel HeatingMaxTemperatureUpgrade;
    [BoxGroup("Common")] public UpgradeLevel HeatingSpeedUpgrade;
    [BoxGroup("Common")] public UpgradeLevel HeatingMinTemperatureUpgrade;

    [Header("Conveyor"), BoxGroup("Common")] public UpgradeLevel SpawnSpeedUpgrade;
    [BoxGroup("Common")] public UpgradeLevel ConveyorSpeedUpgrade;
    [BoxGroup("Common")] public UpgradeLevel BagSizeUpgrade;

    [Header("Earning"), BoxGroup("Common")] public UpgradeLevel EarnForPopUpgrade;
    [BoxGroup("Common")] public UpgradeLevel EarnForBagUpgrade;
    [BoxGroup("Common")] public UpgradeLevel EarnOfflineUpgrade;

    [BoxGroup("Epic")] public UpgradeLevel RepairStoveUpgrade;
    [BoxGroup("Epic")] public UpgradeLevel LuckyBoyUpgrade;
    [BoxGroup("Epic")] public UpgradeLevel MilkyChocoUpgrade;

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

        HeatingMaxTemperatureUpgrade.Level = 0;
        HeatingSpeedUpgrade.Level = 0;
        HeatingMinTemperatureUpgrade.Level = 0;

        SpawnSpeedUpgrade.Level = 0;
        ConveyorSpeedUpgrade.Level = 0;
        BagSizeUpgrade.Level = 0;

        EarnForPopUpgrade.Level = 0;
        EarnForBagUpgrade.Level = 0;
        EarnOfflineUpgrade.Level = 0;

        RepairStoveUpgrade.Level = 0;
        LuckyBoyUpgrade.Level = 0;
        MilkyChocoUpgrade.Level = 0;
    }

    public void Init()
    {
        for (int i = 0; i < 9; i++)
            CommonUpgradeLevels[i].Level = 0;

        for (int i = 0; i < 3; i++)
            EpicUpgradeLevels[i].Level = 0;

        foreach (var itemIn in GameData.Instance.BalanceData.CommonUpgradeData)
            foreach (var itemOut in CommonUpgradeLevels)
                if (itemIn.UpgradeKey == itemOut.UpgradeKey)
                    itemIn.Level = itemOut.Level;

        foreach (var itemIn in GameData.Instance.BalanceData.EpicUpgradeData)
            foreach (var itemOut in EpicUpgradeLevels)
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