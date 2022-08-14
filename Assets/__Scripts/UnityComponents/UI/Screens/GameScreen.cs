using Leopotam.Ecs;
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

    [Header("Bottom Panel")]
    [SerializeField] private HeatingButton heatingButton;

    private void Start()
    {
        heatingButton.OnChangePressState.AddListener((x) =>
        EcsWorld.NewEntity()
        .Get<ChangePressHeatingButtonEvent>()
        .IsPress = x);
    }

    public void UpdateLevelText(int _level) => levelText.text = $"WAVE {_level}";

    public void UpdateMoneyText(double _moneyCount) => moneyText.text = $"{Utility.FormatEveryThirdPower(_moneyCount)}";

    public void UpdateMoneyInSecText(double _moneyInSecCount) => moneyInSecText.text = $"{Utility.FormatEveryThirdPower(_moneyInSecCount)}";

    public void UpdateProgressBar(float _progress) => levelProgressBarFill.fillAmount = _progress;

    public void UpdatePopcornAmountText(double _popcornAmount) => popcornAmountText.text = $"{Utility.FormatEveryThirdPower(_popcornAmount)}";
    public void UpdateGoldPopcornAmountText(int _goldPopcornAmount) => goldPopcornAmountText.text = $"{_goldPopcornAmount}";

    public void UpdateTemperatureText(float _currentTemperature) => temperatureText.text = $"{_currentTemperature}";
    public void UpdateTemperatureProgressBar(float _progress) => temperatureProgressBarFillImage.fillAmount = _progress;

}
