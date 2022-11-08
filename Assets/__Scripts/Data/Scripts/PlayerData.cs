using System;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

[CreateAssetMenu(menuName = "GameData/PlayerData", fileName = "PlayerData")]
public class PlayerData : BaseDataSO
{
    [Header("GameSettings")]
    public bool IsGameLaunchedBefore;
    public bool IsVibrationOn;
    public bool IsSoundOn;

    [Header("Tutorials")]
    public List<bool> TutrorialStates;

    [Header("Money & Popcorn")]
    public double Money;
    public double MoneyInSec;
    public double GoldPopcornAmount;
    public double PopcornAmount;

    [Header("Level")]
    public int CurrentLevelIndex;
    public float CurrentLevelProgress;

    [Header("Heating")] public UpgradeLevelData HeatingPowerUpgrade;
    [Header("SpawnSpeed")] public UpgradeLevelData SpawnSpeedUpgrade;
    [Header("Earning")] public UpgradeLevelData EarnUpgrade;

    [BoxGroup("Epic")] public UpgradeLevelData RepairStoveUpgrade;
    [BoxGroup("Epic")] public UpgradeLevelData LuckyBoyUpgrade;
    [BoxGroup("Epic")] public UpgradeLevelData MilkyChocoUpgrade;

    [Header("Level Upgardes")]
    public List<UpgradeLevelData> CommonUpgradeLevels;
    public List<UpgradeLevelData> EpicUpgradeLevels;

    //[Header("Boosters")] // save it for reload

    public List<bool> ConveyorBuyed;
    public List<bool> ProductLineBuyed;

    public override void ResetData()
    {
        Money = 0;
        MoneyInSec = 0;
        GoldPopcornAmount = 0;
        PopcornAmount = 0;

        CurrentLevelIndex = 0;
        CurrentLevelProgress = 0.0f;

        HeatingPowerUpgrade.Level = 0;
        SpawnSpeedUpgrade.Level = 0;
        EarnUpgrade.Level = 0;

        RepairStoveUpgrade.Level = 0;
        LuckyBoyUpgrade.Level = 0;
        MilkyChocoUpgrade.Level = 0;

        for (int i = 1; i < ConveyorBuyed.Count; i++)
            ConveyorBuyed[i] = false;

        for (int i = 1; i < ProductLineBuyed.Count; i++)
            ConveyorBuyed[i] = false;

        for (int i = 0; i < 3; i++)
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
    }

    public void Init()
    {
        Money = 0;
        MoneyInSec = 0;
        GoldPopcornAmount = 0;
        PopcornAmount = 0;

        CurrentLevelIndex = 0;
        CurrentLevelProgress = 0.0f;

        IsVibrationOn = true;
        IsSoundOn = true;

        foreach (var itemIn in GameData.Instance.BalanceData.CommonUpgradeData)
            foreach (var itemOut in CommonUpgradeLevels)
                if (itemIn.UpgradeKey == itemOut.UpgradeKey)
                    itemIn.Level = itemOut.Level;

        foreach (var itemIn in GameData.Instance.BalanceData.EpicUpgradeData)
            foreach (var itemOut in EpicUpgradeLevels)
                if (itemIn.UpgradeKey == itemOut.UpgradeKey)
                    itemIn.Level = itemOut.Level;

        for (int i = 0; i < 3; i++)
            CommonUpgradeLevels[i].Level = 0;

        for (int i = 0; i < 3; i++)
            EpicUpgradeLevels[i].Level = 0;

        for (int i = 0; i < 4; i++)
            TutrorialStates[i] = false;

        HeatingPowerUpgrade.Level = 0;
        SpawnSpeedUpgrade.Level = 0;
        EarnUpgrade.Level = 0;

        RepairStoveUpgrade.Level = 0;
        LuckyBoyUpgrade.Level = 0;
        MilkyChocoUpgrade.Level = 0;

        for (int i = 0; i < 5; i++)
            ConveyorBuyed[i] = false;
        ConveyorBuyed[0] = true;

        for (int i = 0; i < 3; i++)
            ProductLineBuyed[i] = false;
        ProductLineBuyed[0] = true;
    }

    public void UpdateUpgradeDataLevel()
    {
        for (int i = 0; i < GameData.Instance.BalanceData.CommonUpgradeData.Count; i++)
        {
            if (HeatingPowerUpgrade.UpgradeKey == GameData.Instance.BalanceData.CommonUpgradeData[i].UpgradeKey)
                HeatingPowerUpgrade.Level = GameData.Instance.BalanceData.CommonUpgradeData[i].Level;
            if (SpawnSpeedUpgrade.UpgradeKey == GameData.Instance.BalanceData.CommonUpgradeData[i].UpgradeKey)
                SpawnSpeedUpgrade.Level = GameData.Instance.BalanceData.CommonUpgradeData[i].Level;
            if (EarnUpgrade.UpgradeKey == GameData.Instance.BalanceData.CommonUpgradeData[i].UpgradeKey)
                EarnUpgrade.Level = GameData.Instance.BalanceData.CommonUpgradeData[i].Level;
        }

        for (int i = 0; i < GameData.Instance.BalanceData.EpicUpgradeData.Count; i++)
        {
            if (RepairStoveUpgrade.UpgradeKey == GameData.Instance.BalanceData.CommonUpgradeData[i].UpgradeKey)
                RepairStoveUpgrade.Level = GameData.Instance.BalanceData.CommonUpgradeData[i].Level;
            if (LuckyBoyUpgrade.UpgradeKey == GameData.Instance.BalanceData.CommonUpgradeData[i].UpgradeKey)
                LuckyBoyUpgrade.Level = GameData.Instance.BalanceData.CommonUpgradeData[i].Level;
            if (MilkyChocoUpgrade.UpgradeKey == GameData.Instance.BalanceData.CommonUpgradeData[i].UpgradeKey)
                MilkyChocoUpgrade.Level = GameData.Instance.BalanceData.CommonUpgradeData[i].Level;
        }
    }
}

[Serializable]
public class UpgradeLevelData
{
    public string UpgradeKey;
    public int Level;
}