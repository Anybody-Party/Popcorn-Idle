using System;
using Leopotam.Ecs;
using UnityEngine;

public class OnTriggerEnterMonoLink : PhysicsLinkBase
{
    private void OnTriggerEnter(Collider other)
    {
        if (!_entity.IsAlive()) return;

        _entity.Get<OnTriggerEnterEvent>() = new OnTriggerEnterEvent()
        {
            Collider = other,
            Sender = gameObject
        };
    }
}
