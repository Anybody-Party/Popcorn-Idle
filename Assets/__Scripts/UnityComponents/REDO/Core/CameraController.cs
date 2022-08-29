﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraController : MonoBehaviourSingleton<CameraController>
{
    public Camera camera;
    public GameObject defaultCamera;
    [SerializeField] private Cinemachine.CinemachineImpulseSource shakeSource;

    private void Initialize()
    {

    }

    public void Shake()
    {
        shakeSource.GenerateImpulse(transform.forward);
    }
}
