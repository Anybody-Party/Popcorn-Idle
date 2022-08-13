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
    public double StartMoney;
    public float PopcornSpeed;
    public float SpawnPopTime;
    public float CookingTime;
    public float LaunchPopcornForce;
    public float PopingPopcornForce;
    public float JumpPopcornForce;

    [Header("Tags")]
    [BoxGroup("Tags")] [Tag] public string DespawnTag;
    [BoxGroup("Tags")] [Tag] public string CookingZoneTag;
    [BoxGroup("Tags")] [Tag] public string SellZoneTag;
    [BoxGroup("Tags")] [Tag] public string GroundTag;
    [BoxGroup("Tags")] [Tag] public string ConveyorTag;

    public override void ResetData()
    {
    }

}