using Client;
using Leopotam.Ecs;
using TMPro;
using UnityEngine;

public class LevelCompleteScreen : BaseScreen
{
    [SerializeField] private ActionButton getRewardButton;
    [SerializeField] private ActionButton hideScreenButton;
    [SerializeField] private TextMeshProUGUI rewardMoneyText;

    protected override void ManualStart()
    {
    }

    private void Start()
    {
        getRewardButton.OnClickEvent.AddListener(() =>{
            base.SetShowState(false);
            EcsWorld.NewEntity().Get<GetAdLevelCompleteRewardRequest>();
        });

        hideScreenButton.OnClickEvent.AddListener(() =>{
            base.SetShowState(false);
            EcsWorld.NewEntity().Get<GetLevelCompleteRewardRequest>();
        });
    }

    public void UpdateRewardText(double reward)
    {
        rewardMoneyText.text = $"<sprite=0> {Utility.FormatMoney(reward)}"; // money sprite
    }
}