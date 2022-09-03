using Client;
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
    public Image currencyImage;
    public Sprite moneySprite;
    public Sprite goldPopcornSprite;

    public void InitData(UpgradeData upgradeData, EcsWorld _world)
    {
        upgradeNameText.text = upgradeData.UpgradeName;
        upgradeDescriptionText.text = upgradeData.UpgradeDescription;
        currencyImage.sprite = upgradeData.IsEpicUpgrade ? goldPopcornSprite : moneySprite;
        buyText.text = "BUY";
        upgradeImage.sprite = upgradeData.UpgradeSprite;

        upgradeButton.OnClickEvent.AddListener(() =>
        {
            double price = upgradeData.BasePrice * Mathf.Pow(upgradeData.PriceProgressionCoef, upgradeData.Level);

            if (upgradeData.IsEpicUpgrade)
                _world.NewEntity().Get<SpendGoldPopEvent>().Value = price;
            else
                _world.NewEntity().Get<SpendMoneyEvent>().Value = price;
                
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
        double currency = upgradeData.IsEpicUpgrade ? GameData.Instance.PlayerData.GoldPopcornAmount : GameData.Instance.PlayerData.Money;
        upgradeCounterText.text = $"{upgradeData.Level}/{upgradeData.MaxLevel}";
        buyPriceText.text = $"<sprite=0> {Utility.FormatMoney(price)}"; // money sprite
        upgradeProgressBarFill.fillAmount = (float)upgradeData.Level / (float)upgradeData.MaxLevel;
        if (upgradeButton)
            upgradeButton.SetInteractable(currency >= price && upgradeData.Level < upgradeData.MaxLevel);

        if (upgradeData.Level == upgradeData.MaxLevel)
        {
            buyText.text = "MAX";
            buyPriceText.text = $"";
        }
    }

    public bool CanBuyIt(UpgradeData upgradeData)
    {
        double price = upgradeData.BasePrice * Mathf.Pow(upgradeData.PriceProgressionCoef, upgradeData.Level);
        double currency = upgradeData.IsEpicUpgrade ? GameData.Instance.PlayerData.GoldPopcornAmount : GameData.Instance.PlayerData.Money;

        return currency >= price && upgradeData.Level < upgradeData.MaxLevel;
    }
}
