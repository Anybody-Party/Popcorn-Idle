using Leopotam.Ecs;
using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct ConveyorLink
{
    public int Id;
    [HideInInspector] public int Level;

    public BlendMaterialController StoveMaterial;

    public List<Transform> SpawnPoints;
    public List<Transform> PrepareJumpPoints;
    public List<Transform> JumpPoints;
}