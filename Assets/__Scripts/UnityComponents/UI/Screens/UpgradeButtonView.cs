using Leopotam.Ecs;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class UpgradeButtonView : MonoBehaviour
{
    public ActionButton upgradeButton;
    public TextMeshProUGUI upgradeNameText;
    public TextMeshProUGUI upgradeDescriptionText;
    public TextMeshProUGUI upgradeCounterText;
    public TextMeshProUGUI buyPriceText;
    public TextMeshProUGUI buyText;
    public Image upgradeProgressBarFill;
    public Image upgradeImage;

    public void InitData(UpgradeData upgradeData, EcsWorld _world)
    {
        upgradeNameText.text = upgradeData.UpgradeName;
        upgradeDescriptionText.text = upgradeData.UpgradeDescription;
        buyText.text = "BUY";

        upgradeButton.OnClickEvent.AddListener(() =>
        {
            upgradeData.Level += 1;
            UpdateInfo(upgradeData);

            EcsEntity entity = _world.NewEntity();
            entity.Get<UpgradeEvent>() = new UpgradeEvent
            {
                Key = upgradeData.UpgradeKey,
                Level = upgradeData.Level
            };

        });

        UpdateInfo(upgradeData);
    }

    public void UpdateInfo(UpgradeData upgradeData)
    {
        double price = upgradeData.BasePrice * Mathf.Pow(upgradeData.PriceProgressionCoef, upgradeData.Level);
        upgradeCounterText.text = $"{upgradeData.Level}/{upgradeData.MaxLevel}";
        buyPriceText.text = $"{Utility.FormatMoney(price)}";
        upgradeProgressBarFill.fillAmount = upgradeData.Level / upgradeData.MaxLevel;
        if (upgradeButton)
            upgradeButton.SetInteractable(GameData.Instance.PlayerData.Money >= price && upgradeData.Level < upgradeData.MaxLevel);

        if (upgradeData.Level == upgradeData.MaxLevel)
        {
            buyText.text = "MAX";
            buyPriceText.text = $"";
        }
    }
}
