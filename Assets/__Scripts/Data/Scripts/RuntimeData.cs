using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class RuntimeData : BaseData
{
    public float Temperature;

    public bool IsHeatButtonPressed;
    public bool IsTapSpeedUpWorking;
    public List<int> ReadyToSellCounter;

    public override void ResetData()
    {
    }

    internal void Init()
    {
        ReadyToSellCounter = new List<int>();
        for (int i = 0; i < 3; i++)
            ReadyToSellCounter.Add(0);
    }

    public float GetHeatingSpeed()
    {
        float value = GameData.Instance.BalanceData.BaseHeatingSpeed * Mathf.Pow(GameData.Instance.BalanceData.HeatingSpeedMultiplierForLevel, GameData.Instance.PlayerData.HeatingSpeedUpgrade.Level);
        return value;
    }

    public float GetMaxTemperature()
    {
        float value = GameData.Instance.BalanceData.CurrentTemperatureCap.y + GameData.Instance.PlayerData.HeatingMaxTemperatureUpgrade.Level * GameData.Instance.BalanceData.TemperatureUpgradeStep;
        return value;
    }

    public float GetMinTemperature()
    {
        float value = GameData.Instance.BalanceData.CurrentTemperatureCap.x + GameData.Instance.PlayerData.HeatingMinTemperatureUpgrade.Level * GameData.Instance.BalanceData.TemperatureUpgradeStep;
        return value;
    }

    public float GetPopSpeed()
    {
        float value = GameData.Instance.RuntimeData.IsTapSpeedUpWorking ? GameData.Instance.BalanceData.BasePopcornSpeed * GameData.Instance.BalanceData.TapSpeedUpSpeedCoef : GameData.Instance.BalanceData.BasePopcornSpeed;
        value += GameData.Instance.PlayerData.ConveyorSpeedUpgrade.Level * GameData.Instance.BalanceData.ConveyerSpeedUpgradeBase;
        return value;
    }

    public float GetPopSpawnTime()
    {
        float value = GameData.Instance.BalanceData.BaseSpawnPopTime * Mathf.Pow(GameData.Instance.BalanceData.SpawnTimePopMultiplierForLevel, GameData.Instance.PlayerData.SpawnSpeedUpgrade.Level);
        return value;
    }

    public float GetPopCookingTime()
    {
        float value = (GameData.Instance.BalanceData.BaseCookingTime) * Mathf.Pow(GameData.Instance.BalanceData.CookingTimePopMultiplierForLevel, GetHeatingSpeed());
        return value;
    }

    public double GetPopEarning()
    {
        double value = (GameData.Instance.BalanceData.BasePopSellReward * GameData.Instance.BalanceData.PopSellRewardMultiplier) * Mathf.Pow(GameData.Instance.BalanceData.EarningPopMultiplierForLevel, GameData.Instance.PlayerData.EarnForPopUpgrade.Level);
        return value;
    }

    public int InBigBagPopcornAmount()
    {
        int value = GameData.Instance.BalanceData.BasePopcornsInBigBag + GameData.Instance.PlayerData.BagSizeUpgrade.Level;
        return value;
    }

    public double GetBagEarning()
    {
        double value = ((GameData.Instance.BalanceData.BaseBagSellReward * InBigBagPopcornAmount()) * GameData.Instance.BalanceData.BagSellRewardMultiplier) * Mathf.Pow(GameData.Instance.BalanceData.EarningBagMultiplierForLevel, GameData.Instance.PlayerData.EarnForBagUpgrade.Level);
        return value;
    }

    public float GetChocoMultiplier()
    {
        float value = (GameData.Instance.BalanceData.ChocoPopMultiplier + GameData.Instance.PlayerData.MilkyChocoUpgrade.Level * 0.1f);
        return value;
    }

    public bool GetGoldPopcornChance()
    {
        float value = GameData.Instance.BalanceData.GlodPopcornsChance + GameData.Instance.PlayerData.LuckyBoyUpgrade.Level * 0.01f;
        return value > (UnityEngine.Random.Range(0, 100) / 100.0f);
    }

    public float GetColdingSpeed()
    {
        float value = GameData.Instance.BalanceData.BaseColdingSpeed - GameData.Instance.PlayerData.RepairStoveUpgrade.Level * 0.05f;
        return value;
    }

    [NaughtyAttributes.Button]
    public void PrintUpgrades()
    {
        Debug.Log($"heatingSpeed {GetHeatingSpeed()}");
        Debug.Log($"popcornSpeed {GetPopSpeed()}");
        Debug.Log($"spawnTime {GetPopSpawnTime()}");
        Debug.Log($"cookingTime {GetPopCookingTime()}");
        Debug.Log($"popEarn {GetPopEarning()}");
        Debug.Log($"bagEarn {GetBagEarning()}");
        Debug.Log($"time: {(GetPopSpawnTime() + GetPopCookingTime())} * {(GetPopSpeed() + GetHeatingSpeed())}");
    }
}