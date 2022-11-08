using Client;
using Leopotam.Ecs;
using UnityEngine;
using UnityEngine.UI;

public class CheatScreen : BaseScreen
{
    [SerializeField] private ActionButton getMoneyButton;
    [SerializeField] private ActionButton getGoldButton;
    [SerializeField] private ActionButton goToMidGameButton;
    [SerializeField] private ActionButton goToLateGameButton;
    [SerializeField] private ActionButton resetPlayerDataButton;
    [SerializeField] private ActionButton hideScreenButton;

    protected override void ManualStart()
    {
        getMoneyButton.OnClickEvent.AddListener(() => CheatGetMoney());
        getGoldButton.OnClickEvent.AddListener(() => CheatGetGold());
        goToMidGameButton.OnClickEvent.AddListener(() => CheatGoToMidGame());
        goToLateGameButton.OnClickEvent.AddListener(() => CheatGoToLateGame());
        resetPlayerDataButton.OnClickEvent.AddListener(() => CheatResetPlayerData());
        hideScreenButton.OnClickEvent.AddListener(() => SetShowState(false));
    }

    private void CheatGetMoney()
    {
        EcsWorld.NewEntity().Get<EarnMoneyEvent>().Value = 100000;
    }

    private void CheatGetGold()
    {
        for (int i = 0; i < 100; i++)
            EcsWorld.NewEntity().Get<AddGoldPopEvent>();
    }

    private void CheatGoToMidGame()
    {

        GameData.Instance.PlayerData.HeatingPowerUpgrade.Level = 12;
        GameData.Instance.PlayerData.SpawnSpeedUpgrade.Level = 12;
        GameData.Instance.PlayerData.EarnUpgrade.Level = 12;

        GameData.Instance.PlayerData.RepairStoveUpgrade.Level = 12;
        GameData.Instance.PlayerData.LuckyBoyUpgrade.Level = 12;
        GameData.Instance.PlayerData.MilkyChocoUpgrade.Level = 12;

        for (int i = 1; i < 2; i++)
            EcsWorld.NewEntity().Get<BuyConveyorRequest>().ConveyorId = i;
    }

    private void CheatGoToLateGame()
    {
        GameData.Instance.PlayerData.HeatingPowerUpgrade.Level = 25;
        GameData.Instance.PlayerData.SpawnSpeedUpgrade.Level = 25;
        GameData.Instance.PlayerData.EarnUpgrade.Level = 25;

        GameData.Instance.PlayerData.RepairStoveUpgrade.Level = 25;
        GameData.Instance.PlayerData.LuckyBoyUpgrade.Level = 25;
        GameData.Instance.PlayerData.MilkyChocoUpgrade.Level = 25;

        for (int i = 1; i < 4; i++)
            EcsWorld.NewEntity().Get<BuyConveyorRequest>().ConveyorId = i;
    }

    private void CheatResetPlayerData()
    {
        GameData.Instance.PlayerData.ResetData();
    }
}