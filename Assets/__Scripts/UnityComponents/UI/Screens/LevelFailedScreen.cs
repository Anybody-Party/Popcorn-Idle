using Leopotam.Ecs;
using UnityEngine;

public class LevelFailedScreen : BaseScreen
{
    [SerializeField] private ActionButton restartButton;

    private void Start()
    {
        restartButton.OnClickEvent.AddListener(() =>
        EcsWorld.NewEntity()
        .Get<RestartGameRequest>());
    }
}