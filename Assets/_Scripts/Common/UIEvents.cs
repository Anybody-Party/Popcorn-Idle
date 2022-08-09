using UnityEngine;
using UnityEngine.Events;


public static class UIEvents
{
    //VibrationButton
    public static UnityEvent VibrationButtonTap = new UnityEvent();
    public static UnityEvent<bool> VibrationButtonShow = new UnityEvent<bool>();

    //ExamplePanelShow
    public static UnityEvent<bool> ExamplePanelShow = new UnityEvent<bool>();

    //ExampleText
    public static UnityEvent<string> ChangeExampleText = new UnityEvent<string>();

}
