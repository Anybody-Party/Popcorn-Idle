using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarMonoLink : MonoLink<HealthBarLink>
{
    [SerializeField] private Image fillImage;

    private void OnEnable()
    {
        if (Value.Value == null)
            Value = new HealthBarLink { Value = fillImage };
    }
}
