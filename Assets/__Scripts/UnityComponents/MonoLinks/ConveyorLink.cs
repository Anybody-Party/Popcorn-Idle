using Leopotam.Ecs;
using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct ConveyorLink
{
    public int Id;
    [HideInInspector] public int Level;

    public GameObject Stove;

    public List<Transform> SpawnPoints;
    public List<Transform> FirstListPathPoints;
    public List<Transform> PrepareJumpPoints;
    public List<Transform> JumpPoints;
}