using Leopotam.Ecs;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameScreen : BaseScreen
{
    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private TextMeshProUGUI moneyText;
    [SerializeField] private Image levelProgressBarFill;

    public void UpdateLevelText(int _level)
    {
        levelText.text = $"WAVE {_level}";
    }

    public void UpdateMoneyText(double _moneyCount)
    {
        moneyText.text = $"{Utility.FormatEveryThirdPower(_moneyCount)}";
    }

    public void UpdateProgressBar(float _progress)
    {
        levelProgressBarFill.fillAmount = _progress;
    }
}