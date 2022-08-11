using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct ParticleSystemLink
{
    [SerializeField]
    public List<ParticleSystemWithName> ParticleSystems;
}

[Serializable]
public struct ParticleSystemWithName
{
    public string ParticleSystemName;
    public ParticleSystem ParticleSystem;
}