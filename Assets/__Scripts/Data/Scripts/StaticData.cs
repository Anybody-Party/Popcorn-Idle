using NaughtyAttributes;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "GameData/StaticData", fileName = "StaticData")]
public class StaticData : BaseDataSO
{
    [Header("Prefabs")]
    public GameObject GoldPopcornPrefab;
    public GameObject PopcornPrefab;
    public GameObject EarnInfoPrefab;
    public GameObject EmptyPrefab;

    [Header("Tags")]
    [BoxGroup("Tags")] [Tag] public string DespawnTag;
    [BoxGroup("Tags")] [Tag] public string CookingZoneTag;
    [BoxGroup("Tags")] [Tag] public string SellZoneTag;
    [BoxGroup("Tags")] [Tag] public string GroundTag;
    [BoxGroup("Tags")] [Tag] public string ConveyorTag;
    [BoxGroup("Tags")] [Tag] public string ChocolateAdditionTag;
    [BoxGroup("Tags")] [Tag] public string GoldPopcornTag;

    public override void ResetData()
    {
    }

}