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

    [Header("Data")]
    public int Level;
    public int MaxLevel;
    public double BasePrice;
    public float PriceProgressionCoef;

    public override void ResetData()
    {
        throw new NotImplementedException();
    }
}