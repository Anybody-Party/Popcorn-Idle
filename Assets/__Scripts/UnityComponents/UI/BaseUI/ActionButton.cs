using MoreMountains.NiceVibrations;
using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ActionButton : UIElement
{
    [HideInInspector] public UnityEvent OnClickEvent;

    protected Button unityButton;

    private const string onClickTriggerName = "OnClick";

    private void Start()
    {
        unityButton = GetComponent<Button>();
        unityButton.onClick.AddListener(() => OnClickEvent.Invoke());
        OnClickEvent.AddListener(OnClickEventReaction);
    }

    private void OnClickEventReaction()
    {
        MMVibrationManager.Haptic(HapticTypes.MediumImpact);
        animator.SetTrigger(onClickTriggerName);
    }

    private void OnDestroy()
    {
        unityButton?.onClick.RemoveAllListeners();
        OnClickEvent.RemoveAllListeners();
    }
}