using Leopotam.Ecs;
using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct HandLink
{
    public int ProductLineId;
    public List<GameObject> Bags;
    public ParticleSystem EarnMoneyPS;
}