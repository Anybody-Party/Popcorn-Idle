using Leopotam.Ecs;
using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct ConveyorLink
{
    public int Id;
    [HideInInspector] public bool IsBuyed;

    public BlendMaterialController StoveMaterial;
    public GameObject HeatSteam;
    public GameObject AirHeatDistortion;

    public List<Transform> SpawnPoints;
    public List<Transform> PrepareJumpPoints;
    public List<Transform> JumpPoints;
}