using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public static class GlobalEvents
{
    //Example
    public static UnityEvent<int> ExampleAddMoney = new UnityEvent<int>();
    public static UnityEvent<int> ExampleMoneyAdded = new UnityEvent<int>();

    //Camera
    public static UnityEvent MoveCameraToExamplePos = new UnityEvent();

    //Settings
    public static UnityEvent<bool> SetVibrationState = new UnityEvent<bool>();
}
