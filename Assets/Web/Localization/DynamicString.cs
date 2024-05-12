using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Components;

[RequireComponent(typeof(LocalizeStringEvent))]
public class DynamicString : MonoBehaviour
{
    private LocalizeStringEvent _localizeStringEvent;
    [SerializeField] private string key;
    
    private Dictionary<string, int> dictionaryInt = new Dictionary<string, int>();
    private Dictionary<string, float> dictionaryFloat = new Dictionary<string, float>();
    private Dictionary<string, double> dictionaryDouble = new Dictionary<string, double>();
    private Dictionary<string, string> dictionaryString = new Dictionary<string, string>();

    private void Awake()
    {
        _localizeStringEvent = GetComponent<LocalizeStringEvent>();
        dictionaryInt.Add(key, 0);
        dictionaryFloat.Add(key, 0);
        dictionaryDouble.Add(key, 0);
        dictionaryString.Add(key, "");
    }

    public void UpdateString(int value)
    {
        LocalizedString localizedString = _localizeStringEvent.StringReference;
        dictionaryInt[key] = value;
        localizedString.Arguments = new List<object> { dictionaryInt };
        localizedString.RefreshString();
    }

    public void UpdateString(float value)
    {
        LocalizedString localizedString = _localizeStringEvent.StringReference;
        dictionaryFloat[key] = value;
        localizedString.Arguments = new List<object> { dictionaryFloat };
        localizedString.RefreshString();
    }
    
    public void UpdateString(double value)
    {
        LocalizedString localizedString = _localizeStringEvent.StringReference;
        dictionaryDouble[key] = value;
        localizedString.Arguments = new List<object> { dictionaryDouble };
        localizedString.RefreshString();
    }
    
    public void UpdateString(string value)
    {
        LocalizedString localizedString = _localizeStringEvent.StringReference;
        dictionaryString[key] = value;
        localizedString.Arguments = new List<object> { dictionaryString };
        localizedString.RefreshString();
    }

}