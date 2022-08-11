using Leopotam.Ecs;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameUI : MonoBehaviour
{
    [SerializeField] public LevelCompleteScreen LevelCompleteScreen;
    [SerializeField] public LevelFailedScreen LevelFailedScreen;
    [SerializeField] public GameScreen GameScreen;
    [SerializeField] public VibrationButtonScreen VibrationButtonScreen;

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
    public void SetShowStateLevelFailedScreen(bool isShow) => LevelFailedScreen.SetShowState(isShow);
    public void SetShowStateGameScreen(bool isShow) => GameScreen.SetShowState(isShow);
    public void SetShowStateVirationButtonScreen(bool isShow) => VibrationButtonScreen.SetShowState(isShow);

    [NaughtyAttributes.Button]
    public void TestShowLevelComplete()
    {
        LevelCompleteScreen.SetShowState(true);
    }
}
