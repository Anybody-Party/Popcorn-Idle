using Leopotam.Ecs;
using TMPro;
using UnityEngine;

public class OfflineBonusScreen : BaseScreen
{
    [SerializeField] private ActionButton getRewardButton;
    [SerializeField] private ActionButton hideScreenButton;
    [SerializeField] private TextMeshProUGUI rewardMoneyText;

    private void Start()
    {
        getRewardButton.OnClickEvent.AddListener(() =>
        EcsWorld.NewEntity()
        .Get<GetOfflineBonusRewardRequest>().Multiplier = 3);

        hideScreenButton.OnClickEvent.AddListener(() =>
        EcsWorld.NewEntity()
        .Get<GetOfflineBonusRewardRequest>().Multiplier = 1);

        getRewardButton.OnClickEvent.AddListener(() => SetShowState(false));
        hideScreenButton.OnClickEvent.AddListener(() => SetShowState(false));
    }

    public void UpdateRewardText(double reward)
    {
        rewardMoneyText.text = $"<sprite=0> {Utility.FormatMoney(reward)}"; // money sprite
    }
}