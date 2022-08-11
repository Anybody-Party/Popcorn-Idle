using Leopotam.Ecs;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MonoLinkBase : MonoBehaviour
{
    public abstract void Make(ref EcsEntity entity);
}