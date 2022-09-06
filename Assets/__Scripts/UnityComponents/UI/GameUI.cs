using Leopotam.Ecs;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameUI : MonoBehaviour
{
    [SerializeField] public LevelCompleteScreen LevelCompleteScreen;
    [SerializeField] public OfflineBonusScreen OfflineBonusScreen;
    [SerializeField] public GameScreen GameScreen;
    [SerializeField] public UpgradeScreen UpgradeScreen;
    [SerializeField] public SettingScreen SettingScreen;
    [SerializeField] public CheatScreen CheatScreen;

    [SerializeField] public HeatingTutorialScreen HeatingTutorialScreen;
    [SerializeField] public UpgradeTutorialScreen UpgradeTutorialScreen;

    private List<BaseScreen> screens;

    public void InjectEcsWorld(EcsWorld ecsWorld)
    {
        screens = new List<BaseScreen>();
        screens.AddRange(GetComponentsInChildren<BaseScreen>(true));
        foreach (var screen in screens)
        {
            screen.gameObject.SetActive(true);
            screen.Init(ecsWorld);
            screen.gameObject.SetActive(false);
        }
    }

    public void SetShowStateLevelCompleteScreen(bool isShow) => LevelCompleteScreen.SetShowState(isShow);
    public void SetShowStateGameScreen(bool isShow) => GameScreen.SetShowState(isShow);
    public void SetShowStateUpgradeScreen(bool isShow) => UpgradeScreen.SetShowState(isShow);
    public void SetShowStateSettingScreen(bool isShow) => SettingScreen.SetShowState(isShow);
    public void SetShowStateCheatScreen(bool isShow) => CheatScreen.SetShowState(isShow);

    public void SetShowStateHeatingTutorialScreen(bool isShow) => HeatingTutorialScreen.SetShowState(isShow);
    public void SetShowStateUpgradeTutorialScreen(bool isShow) => UpgradeTutorialScreen.SetShowState(isShow);

    public void SetShowStateOfflineBonusScreen(bool isShow, double reward)
    {
        OfflineBonusScreen.SetShowState(isShow);
        OfflineBonusScreen.UpdateRewardText(reward);
    }
}
