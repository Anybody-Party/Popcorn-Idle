using Leopotam.Ecs;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldGameUI : MonoBehaviour
{
    [SerializeField] public List<BuyConveyorScreen> BuyConveyorScreens;

    private List<BaseScreen> screens;

    public void InjectEcsWorld(EcsWorld ecsWorld)
    {
        screens = new List<BaseScreen>();
        screens.AddRange(GetComponentsInChildren<BaseScreen>(true));
        foreach (var screen in screens)
        {
            screen.gameObject.SetActive(true);
            screen.InjectEcsWorld(ecsWorld);
            screen.Init();
            screen.gameObject.SetActive(false);
        }
    }

    public void UpdateBuyConveyorScreens()
    {
        foreach (var item in BuyConveyorScreens)
            item.UpdateButtonView();
    }


}
