using Leopotam.Ecs;
using MoreMountains.NiceVibrations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public abstract class BaseScreen : UIElement
{
    protected bool ScreenIsShow;

    [HideInInspector] public EcsWorld EcsWorld;

    [HideInInspector] public Action OnHideScreen;
    [HideInInspector] public Action OnShowScreen;

    public void Init(EcsWorld ecsWorld)
    {
        EcsWorld = ecsWorld;
        ScreenIsShow = false;
    }

    public override void SetShowState(bool _isShow) // [System.Runtime.CompilerServices.CallerMemberName] string memberName = "" - WHO
    {
        base.SetShowState(_isShow);
        if (_isShow)
        {
            OnShowScreen?.Invoke();
            ScreenIsShow = true;
        }
        else
        {
            OnHideScreen?.Invoke();
            ScreenIsShow = false;
        }
    }
}
