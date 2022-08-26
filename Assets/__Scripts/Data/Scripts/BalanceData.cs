using NaughtyAttributes;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "GameData/BalanceData", fileName = "BalanceData")]
public class BalanceData : BaseDataSO
{
    [Header("Base Balance")]
    public float BasePopcornSpeed;
    public float TapSpeedUpSpeedCoef;
    public float BaseSpawnPopTime;
    public float BaseCookingTime;

    public float BaseHeatingSpeed;
    public float BaseColdingSpeed;
    [MinMaxSlider(1.0f, 75.0f)]
    public Vector2 CurrentTemperatureCap;
    public float MaxTemperature;

    [Header("Money Balance")]
    public double StartMoney;

    public double BasePopSellReward;
    public double PopSellRewardMultiplier;

    public double BaseBagSellReward;
    public double BagSellRewardMultiplier;

    [Header("Physic Balance")]
    [MinMaxSlider(1.0f, 10.0f)]
    public Vector2 LaunchPopcornForce;
    [MinMaxSlider(1.0f, 10.0f)]
    public Vector2 PopingPopcornForce;
    [MinMaxSlider(1.0f, 10.0f)]
    public Vector2 JumpPopcornForce;
    [MinMaxSlider(0.0f, 3.0f)]
    public Vector2 CookingShakePopcornForce;

    [Header("Common Upgrade Data")]
    public List<UpgradeData> CommonUpgradeData;

    [Header("Epic Upgrade Data")]
    public List<UpgradeData> EpicUpgradeData;

    public override void ResetData()
    {
    }
}