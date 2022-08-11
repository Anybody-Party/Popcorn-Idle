using NaughtyAttributes;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "GameData/StaticData", fileName = "StaticData")]
public class StaticData : BaseDataSO
{
    [Header("GameSettings")]
    public bool IsVibrationOn;

    [Header("Prefabs")]
    public GameObject PopcornPrefab;
    public GameObject EmptyPrefab;

    [Header("Balance")]
    public int StartMoney;
    public int PopcornSpeed;
    public int MakingPopcornTime;
    public int HeatingSpeed;

    [Header("Tags")]
    [BoxGroup("Tags")] [Tag] public string DespawnTag;
    [BoxGroup("Tags")] [Tag] public string GroundTag;

    public override void ResetData()
    {
    }

}