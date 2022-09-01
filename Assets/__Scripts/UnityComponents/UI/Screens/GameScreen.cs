using DG.Tweening;
using Leopotam.Ecs;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameScreen : BaseScreen
{
    [Header("Top Panel")]
    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private TextMeshProUGUI moneyText;
    [SerializeField] private TextMeshProUGUI moneyInSecText;

    [SerializeField] private TextMeshProUGUI popcornAmountText;
    [SerializeField] private TextMeshProUGUI goldPopcornAmountText;

    [SerializeField] private TextMeshProUGUI temperatureText;
    [SerializeField] private Image temperatureProgressBarFillImage;

    [SerializeField] private Image levelProgressBarFill;

    [SerializeField] private Image goldPopImage;
    [SerializeField] private Transform goldPopPanel;

    [SerializeField] private ActionButton openSettingsScreenButton;

    [Header("Heating Button")]
    [SerializeField] private HeatingButton heatingButton;
    [SerializeField] private Image heatingButtonImage;
    [SerializeField] private Gradient heatingButtonColorGradient;

    [Header("Show Upgrades Screen Button")]
    [SerializeField] private ActionButton showUpgradeScreenButton;

    private void Start()
    {
        heatingButton.OnChangePressState.AddListener((x) =>
        {
            EcsEntity eventEntity = EcsWorld.NewEntity();
            GameData.Instance.RuntimeData.IsHeatButtonPressed = x;
            if (x)
                eventEntity.Get<PressHeatingButtonEvent>();
            else
                eventEntity.Get<ReleaseHeatingButtonEvent>();
        });

        showUpgradeScreenButton.OnClickEvent.AddListener(() =>
        {
            EcsWorld.NewEntity().Get<ShowUpgradeScreenRequest>();
        });

        openSettingsScreenButton.OnClickEvent.AddListener(() =>
        {
            EcsWorld.NewEntity().Get<ShowSettingScreenRequest>();
        });
    }

    public void UpdateLevelText(int _level) => levelText.text = $"WAVE {_level}";

    public void UpdateMoneyText(double _moneyCount) => moneyText.text = $"{Utility.FormatMoney(_moneyCount)}";

    public void UpdateMoneyInSecText(double _moneyInSecCount) => moneyInSecText.text = $"{Utility.FormatMoney(_moneyInSecCount)}/SEC";

    public void UpdateProgressBar(float _progress) => levelProgressBarFill.fillAmount = _progress;

    public void UpdatePopcornAmountText(double _popcornAmount) => popcornAmountText.text = $"{Utility.FormatMoney(_popcornAmount)}";
    public void UpdateGoldPopcornAmountText(double _goldPopcornAmount) => goldPopcornAmountText.text = $"{_goldPopcornAmount}";

    public void UpdateTemperatureText(float _currentTemperature) => temperatureText.text = $"{179 + _currentTemperature:0}";
    public void UpdateTemperatureProgressBar(float _temperature) => temperatureProgressBarFillImage.fillAmount = _temperature;
    public void UpdateHeatingButtonColor(float _temperature) => heatingButtonImage.color = heatingButtonColorGradient.Evaluate(_temperature);

    public Vector3 GetGoldPopPosition() => goldPopImage.transform.position;
    public void BounceGoldPopcron() => goldPopPanel.transform.DOPunchScale(Vector3.one * 1.1f, 0.2f);
}
