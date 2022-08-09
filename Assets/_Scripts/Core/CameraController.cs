using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraController : MonoBehaviourSingleton<CameraController>
{
    public GameObject defaultCamera;
    [SerializeField] private Cinemachine.CinemachineImpulseSource shakeSource;

    private void Initialize()
    {

    }

    public void CameraShake()
    {
        shakeSource.GenerateImpulse(transform.forward);
    }
}
