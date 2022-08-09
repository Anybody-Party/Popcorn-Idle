using RH.Utilities.Coroutines;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExamplePanel : BasePanel
{
    protected override void Init()
    {
        panelIsOpen = true;
        UIEvents.ExamplePanelShow.AddListener(SetShow);
    }
}
