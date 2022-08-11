using Leopotam.Ecs;
using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct ConveyorLink
{
    public int No;

    public Material StoveMaterial;

    public List<Transform> SpawnPoints;
    public List<Transform> FirstListPathPoints;
}