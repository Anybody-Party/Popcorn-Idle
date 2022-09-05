using NaughtyAttributes;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "GameData/UpgradeData", fileName = "UpgradeData")]
[Serializable]
public class UpgradeData : BaseDataSO
{
    public string UpgradeKey;

    [Header("View")]
    public string UpgradeName;
    public string UpgradeDescription;
    public Sprite UpgradeSprite;

    [Header("Joke")]
    public string JokeUpgradeName;
    public string JokeUpgradeDescription;

    [Header("Data")]
    public int Level;
    public int MaxLevel;
    public bool IsEpicUpgrade;
    public double BasePrice;
    public float PriceProgressionCoef;

    [Header("Progression")]
    public float MultiplierForLevel;
    public float StartValue;
    public float StepValue;

    public bool CanBuyIt()
    {
        double price = BasePrice * Mathf.Pow(PriceProgressionCoef, Level);
        double currency = IsEpicUpgrade ? GameData.Instance.PlayerData.GoldPopcornAmount : GameData.Instance.PlayerData.Money;

        return currency >= price && Level < MaxLevel;
    }

    public override void ResetData()
    {
        throw new NotImplementedException();
    }
}