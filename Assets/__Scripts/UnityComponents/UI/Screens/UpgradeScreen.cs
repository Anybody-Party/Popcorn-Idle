using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeScreen : BaseScreen
{
    [Header("Common")]
    [SerializeField] private GameObject commonTab;
    [SerializeField] private ActionButton commonTabButton;
    [SerializeField] private List<UpgradeButtonView> commonUpgradeButtons;
    [SerializeField] private ScrollRect commonScrollRect;

    [Header("Epic")]
    [SerializeField] private GameObject epicTab;
    [SerializeField] private ActionButton epicTabButton;
    [SerializeField] private List<UpgradeButtonView> epicUpgradeButtons;
    [SerializeField] private ScrollRect epicScrollRect;

    [SerializeField] private ActionButton hideScreenButton;
    [SerializeField] private GameObject CanBuyUpgrade;

    private void Start()
    {
        for (int i = 0; i < commonUpgradeButtons.Count; i++)
            commonUpgradeButtons[i].InitData(GameData.Instance.BalanceData.CommonUpgradeData[i], EcsWorld);

        for (int i = 0; i < epicUpgradeButtons.Count; i++)
            epicUpgradeButtons[i].InitData(GameData.Instance.BalanceData.EpicUpgradeData[i], EcsWorld);

        commonTabButton.OnClickEvent.AddListener(() => UpdateCommonTab());
        epicTabButton.OnClickEvent.AddListener(() => UpdateEpicTab());
        hideScreenButton.OnClickEvent.AddListener(() => SetShowState(false));

        OnShowScreen.AddListener(() => UpdateCommonTab());
    }

    private void UpdateCommonTab()
    {
        commonTab.SetActive(true);
        epicTab.SetActive(false);
        Utility.ScrollToTop(commonScrollRect);
        for (int i = 0; i < commonUpgradeButtons.Count; i++)
            commonUpgradeButtons[i].UpdateInfo(GameData.Instance.BalanceData.CommonUpgradeData[i]);
    }

    private void UpdateEpicTab()
    {
        commonTab.SetActive(false);
        epicTab.SetActive(true);
        Utility.ScrollToTop(epicScrollRect);
        for (int i = 0; i < epicUpgradeButtons.Count; i++)
            epicUpgradeButtons[i].UpdateInfo(GameData.Instance.BalanceData.EpicUpgradeData[i]);
    }

    private void Update()
    {
        if (commonTab.activeInHierarchy)
            for (int i = 0; i < commonUpgradeButtons.Count; i++)
                commonUpgradeButtons[i].UpdateInfo(GameData.Instance.BalanceData.CommonUpgradeData[i]);

        if (epicTab.activeInHierarchy)
            for (int i = 0; i < epicUpgradeButtons.Count; i++)
                epicUpgradeButtons[i].UpdateInfo(GameData.Instance.BalanceData.EpicUpgradeData[i]);

        bool canBuyUpgrade = false;

        for (int i = 0; i < GameData.Instance.BalanceData.EpicUpgradeData.Count; i++)
            if (commonUpgradeButtons[i].CanBuyIt(GameData.Instance.BalanceData.CommonUpgradeData[i]))
                canBuyUpgrade = true;

        for (int i = 0; i < GameData.Instance.BalanceData.EpicUpgradeData.Count; i++)
            if (epicUpgradeButtons[i].CanBuyIt(GameData.Instance.BalanceData.CommonUpgradeData[i]))
                canBuyUpgrade = true;

        if (canBuyUpgrade && !CanBuyUpgrade.activeInHierarchy)
            CanBuyUpgrade.SetActive(canBuyUpgrade);
        if (!canBuyUpgrade && CanBuyUpgrade.activeInHierarchy)
            CanBuyUpgrade.SetActive(canBuyUpgrade);
    }
}
