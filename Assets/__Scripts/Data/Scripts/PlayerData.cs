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

    [Header("Heating"), BoxGroup("Common")] public UpgradeLevelData HeatingMaxTemperatureUpgrade;
    [BoxGroup("Common")] public UpgradeLevelData HeatingSpeedUpgrade;
    [BoxGroup("Common")] public UpgradeLevelData HeatingMinTemperatureUpgrade;

    [Header("Conveyor"), BoxGroup("Common")] public UpgradeLevelData SpawnSpeedUpgrade;
    [BoxGroup("Common")] public UpgradeLevelData ConveyorSpeedUpgrade;
    [BoxGroup("Common")] public UpgradeLevelData BagSizeUpgrade;

    [Header("Earning"), BoxGroup("Common")] public UpgradeLevelData EarnForPopUpgrade;
    [BoxGroup("Common")] public UpgradeLevelData EarnForBagUpgrade;
    [BoxGroup("Common")] public UpgradeLevelData EarnOfflineUpgrade;

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

        for (int i = 1; i < ConveyorBuyed.Count; i++)
            ConveyorBuyed[i] = false;

        for (int i = 1; i < ProductLineBuyed.Count; i++)
            ConveyorBuyed[i] = false;

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

        for (int i = 0; i < 9; i++)
            CommonUpgradeLevels[i].Level = 0;

        for (int i = 0; i < 3; i++)
            EpicUpgradeLevels[i].Level = 0;

        for (int i = 0; i < 4; i++)
            TutrorialStates[i] = false;

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
            if (HeatingMaxTemperatureUpgrade.UpgradeKey == GameData.Instance.BalanceData.CommonUpgradeData[i].UpgradeKey)
                HeatingMaxTemperatureUpgrade.Level = GameData.Instance.BalanceData.CommonUpgradeData[i].Level;
            if (HeatingSpeedUpgrade.UpgradeKey == GameData.Instance.BalanceData.CommonUpgradeData[i].UpgradeKey)
                HeatingSpeedUpgrade.Level = GameData.Instance.BalanceData.CommonUpgradeData[i].Level;
            if (HeatingMinTemperatureUpgrade.UpgradeKey == GameData.Instance.BalanceData.CommonUpgradeData[i].UpgradeKey)
                HeatingMinTemperatureUpgrade.Level = GameData.Instance.BalanceData.CommonUpgradeData[i].Level;

            if (SpawnSpeedUpgrade.UpgradeKey == GameData.Instance.BalanceData.CommonUpgradeData[i].UpgradeKey)
                SpawnSpeedUpgrade.Level = GameData.Instance.BalanceData.CommonUpgradeData[i].Level;
            if (ConveyorSpeedUpgrade.UpgradeKey == GameData.Instance.BalanceData.CommonUpgradeData[i].UpgradeKey)
                ConveyorSpeedUpgrade.Level = GameData.Instance.BalanceData.CommonUpgradeData[i].Level;
            if (BagSizeUpgrade.UpgradeKey == GameData.Instance.BalanceData.CommonUpgradeData[i].UpgradeKey)
                BagSizeUpgrade.Level = GameData.Instance.BalanceData.CommonUpgradeData[i].Level;

            if (EarnForPopUpgrade.UpgradeKey == GameData.Instance.BalanceData.CommonUpgradeData[i].UpgradeKey)
                EarnForPopUpgrade.Level = GameData.Instance.BalanceData.CommonUpgradeData[i].Level;
            if (EarnForBagUpgrade.UpgradeKey == GameData.Instance.BalanceData.CommonUpgradeData[i].UpgradeKey)
                EarnForBagUpgrade.Level = GameData.Instance.BalanceData.CommonUpgradeData[i].Level;
            if (EarnOfflineUpgrade.UpgradeKey == GameData.Instance.BalanceData.CommonUpgradeData[i].UpgradeKey)
                EarnOfflineUpgrade.Level = GameData.Instance.BalanceData.CommonUpgradeData[i].Level;
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