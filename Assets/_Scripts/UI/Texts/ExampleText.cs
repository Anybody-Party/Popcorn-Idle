using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExampleText : BaseText
{
    protected override void Init()
    {
        UIEvents.ChangeExampleText.AddListener(UpdateText);
    }
}
