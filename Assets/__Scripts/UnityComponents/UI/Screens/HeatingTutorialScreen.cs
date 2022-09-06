using Client;
using DG.Tweening;
using Leopotam.Ecs;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HeatingTutorialScreen : BaseScreen
{
    [SerializeField] private ActionButton completeTutorialButton;

    private void Start()
    {
        completeTutorialButton.OnClickEvent.AddListener(() =>
        {
            EcsWorld.NewEntity().Get<CompleteTutorialEvent>().Tutorial = StaticData.Tutorials.Heating;
            SetShowState(false);
        });
    }
}
