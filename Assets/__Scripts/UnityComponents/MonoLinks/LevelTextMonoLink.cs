using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LevelTextMonoLink : MonoLink<LevelTextLink>
{
    [SerializeField] private TextMeshProUGUI text;

    private void OnEnable()
    {
        if (Value.Value == null)
            Value = new LevelTextLink { Value = text };
    }
}
