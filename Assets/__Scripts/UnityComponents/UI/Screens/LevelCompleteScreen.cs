using Leopotam.Ecs;
using UnityEngine;

public class LevelCompleteScreen : BaseScreen
{
    [SerializeField] private ActionButton getRewardButton;
    [SerializeField] private ActionButton hideScreenButton;
    [SerializeField] private BaseText rewardMoneyText;

    private void Start()
    {
        //getRewardButton.OnClickEvent.AddListener(() =>
        //EcsWorld.NewEntity()
        //.Get<CreateNewLevelRequest>()
        //.IsRestart = false);

        //hideScreenButton.OnClickEvent.AddListener(() =>
        //EcsWorld.NewEntity()
        //.Get<CreateNewLevelRequest>()
        //.IsRestart = false);
    }
}